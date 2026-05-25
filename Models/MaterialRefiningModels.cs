namespace CraftingCalc.Models;

public class MaterialRefiningSettings
{
    public RawResourceType ResourceType { get; set; } = RawResourceType.Wood;
    public int TargetTier { get; set; } = 6;
    public int TargetEnchant { get; set; } = 0;
    public int Quantity { get; set; } = 1;
    public string City { get; set; } = "Fort Sterling";
    public bool UseBuyOrder { get; set; } = false;

    // Silver charged by the refining station per 100 nutrition consumed.
    public decimal StationFeePer100Nutrition { get; set; } = 0m;

    // Chain control: refine from this tier up to TargetTier (always chain-refines
    // the target). Tiers strictly below ChainFromTier (and the T3 input for the
    // T4 step) are sourced from market. Clamped to <= TargetTier in the UI.
    public int ChainFromTier { get; set; } = 4;

    // Return rate inputs — uses the Local Production Bonus (LPB) formula:
    //   effective_output_multiplier = 1 + LPB
    //   effective_return_rate       = LPB / (1 + LPB)
    //   effective_cost              = raw_cost / (1 + LPB)
    // All LPB components are additive; same formula as gear crafting.
    public bool UseFocus { get; set; } = false;
    public decimal FocusLpb { get; set; } = 0.59m;
    public decimal BaseLocalProductionBonus { get; set; } = 0.18m; // city base
    public decimal BonusDay { get; set; } = 0m;                    // 0 / 0.10 / 0.20
    public decimal BonusCityBonus { get; set; } = 0m;              // 0.40 when refining in matching royal city

    // Heart recipe — replaces 1 raw with 1 heart per refined output.
    // Not available for enchant 4. Applies to every chain step (when its enchant allows it).
    // Auto: per-step, heart is applied when HeartPrice < RawMarketPrice.
    public HeartMode HeartMode { get; set; } = HeartMode.Off;
    public long HeartPrice { get; set; } = 0;
}

public enum HeartMode
{
    Off,
    Auto,
    On,
}

public class MaterialRefiningNode
{
    public int Tier { get; set; }
    public int Enchant { get; set; }
    public string RefinedApiId { get; set; } = "";
    public string RawApiId { get; set; } = "";

    public long RefinedMarketPrice { get; set; }
    public long RefinedApiMarketPrice { get; set; }
    public bool RefinedManualPrice { get; set; }
    public Dictionary<string, long> RefinedCityPrices { get; set; } = new();

    public long RawMarketPrice { get; set; }
    public long RawApiMarketPrice { get; set; }
    public bool RawManualPrice { get; set; }
    public Dictionary<string, long> RawCityPrices { get; set; } = new();

    // Computed by calculator
    public decimal ChainRefineUnitCost { get; set; } // cost per refined unit if chain-refined here
    public bool HasChainCost { get; set; }
    public decimal EffectiveUnitCost { get; set; }   // cost the parent chain uses for this node
    public bool EffectiveFromMarket { get; set; }    // true → market price; false → chain refined

    public string TierLabel => Enchant == 0 ? $"T{Tier}" : $"T{Tier}.{Enchant}";
    public bool HasRefinedMarket => RefinedMarketPrice > 0;
    public bool HasRawMarket => RawMarketPrice > 0;
}

public class MaterialRefiningStep
{
    public int Tier { get; set; }
    public int Enchant { get; set; }

    public int RawCount { get; set; }
    public long RawUnitPrice { get; set; }

    public int HeartCount { get; set; }
    public long HeartUnitPrice { get; set; }

    public int LowerCount { get; set; }
    public long LowerUnitPrice { get; set; }
    public int LowerTier { get; set; }
    public int LowerEnchant { get; set; }
    public bool LowerFromMarket { get; set; }

    public decimal ReturnRate { get; set; }        // 0..1 (final stacked RRR for this step)
    public long StationFeePerOutputUnit { get; set; }
    public decimal CostPerOutputUnit { get; set; } // expected cost per 1 refined output unit (after RRR)

    public string TierLabel => Enchant == 0 ? $"T{Tier}" : $"T{Tier}.{Enchant}";
    public string LowerLabel => LowerEnchant == 0 ? $"T{LowerTier}" : $"T{LowerTier}.{LowerEnchant}";
}

public class MaterialRefiningResult
{
    public bool Found { get; set; }
    public bool DirectBuy { get; set; }
    public List<MaterialRefiningStep> Steps { get; set; } = new();

    public decimal UnitCost { get; set; }
    public decimal TotalCost { get; set; }

    public long SellPriceUnit { get; set; }
    public decimal ProfitPerUnit { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal RoiPercent { get; set; }

    public long DirectBuyUnitPrice { get; set; }
    public decimal SavingsVsDirect { get; set; }
}

public class MaterialRefiningGrid
{
    public RawResourceType ResourceType { get; set; }
    public List<MaterialRefiningNode> Nodes { get; set; } = new(); // T4-T8 × E0-E4

    // T3 refined material (no enchanted versions exist in-game) — used as input for T4 refining
    public string T3RefinedApiId { get; set; } = "";
    public long T3RefinedMarketPrice { get; set; }
    public long T3RefinedApiMarketPrice { get; set; }
    public bool T3RefinedManualPrice { get; set; }
    public Dictionary<string, long> T3RefinedCityPrices { get; set; } = new();

    public MaterialRefiningNode? GetNode(int tier, int enchant) =>
        Nodes.FirstOrDefault(n => n.Tier == tier && n.Enchant == enchant);
}