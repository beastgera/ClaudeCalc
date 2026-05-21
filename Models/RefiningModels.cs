namespace CraftingCalc.Models;

public enum RawResourceType
{
    Wood,
    Ore,
    Hide,
    Fiber
}

public class RefiningSettings
{
    public RawResourceType ResourceType { get; set; } = RawResourceType.Wood;
    public int TargetTier { get; set; } = 6;
    public int TargetEnchant { get; set; } = 0;
    public int Quantity { get; set; } = 100;
    public string City { get; set; } = "Caerleon";
    public bool UseBuyOrder { get; set; } = false;
    public decimal GlobalDiscount { get; set; } = 0m; // -1 .. 1 (fraction off base fee; negative = surcharge)
    public decimal StationFeePer100Nutrition { get; set; } = 0m; // silver per 100 nutrition charged by the station

    // Optional source override: when both set, the calc starts from this exact node
    // (its market price is used, or 1 silver as a fallback if no price is set).
    public int? StartTier { get; set; }
    public int? StartEnchant { get; set; }
    public bool HasForcedSource => StartTier.HasValue && StartEnchant.HasValue;
}

public class RefiningNode
{
    public int Tier { get; set; }
    public int Enchant { get; set; }
    public string ApiId { get; set; } = "";

    public long MarketPrice { get; set; }       // current price used by calc (may be manual or API)
    public long ApiMarketPrice { get; set; }    // last value pulled from API
    public bool ManualPrice { get; set; }       // user typed it
    public Dictionary<string, long> CityPrices { get; set; } = new();

    public long CheapestUnitCost { get; set; } = long.MaxValue;
    public bool CheapestIsDirectBuy { get; set; }
    public int CheapestFromTier { get; set; } = -1;
    public int CheapestFromEnchant { get; set; } = -1;

    public string TierLabel => Enchant == 0 ? $"T{Tier}" : $"T{Tier}.{Enchant}";
    public bool HasMarketPrice => MarketPrice > 0;
    public bool HasCheapest => CheapestUnitCost != long.MaxValue;
}

public class TransmuteStep
{
    public int FromTier { get; set; }
    public int FromEnchant { get; set; }
    public int ToTier { get; set; }
    public int ToEnchant { get; set; }
    public long SilverFeePerUnit { get; set; }
    public long StationFeePerUnit { get; set; }
    public long FeePerUnit => SilverFeePerUnit + StationFeePerUnit;
    public long RunningUnitCost { get; set; } // cost-per-unit AFTER this step

    public string Kind => FromTier != ToTier ? "Tier up" : "Enchant up";
    public string FromLabel => FromEnchant == 0 ? $"T{FromTier}" : $"T{FromTier}.{FromEnchant}";
    public string ToLabel   => ToEnchant   == 0 ? $"T{ToTier}"   : $"T{ToTier}.{ToEnchant}";
}

public class RefiningPath
{
    public bool Found { get; set; }
    public int SourceTier { get; set; }
    public int SourceEnchant { get; set; }
    public string SourceApiId { get; set; } = "";
    public long SourceUnitPrice { get; set; }
    public List<TransmuteStep> Steps { get; set; } = new();
    public long UnitCost { get; set; }        // total cost per unit of target
    public long TotalCost { get; set; }       // unit cost × quantity
    public long DirectBuyUnitCost { get; set; }   // price-per-unit if bought directly (0 if unknown)
    public long DirectBuyTotalCost { get; set; }  // direct × quantity
    public long Savings { get; set; }         // direct total - cheapest total (0 if direct cheaper or unknown)
    public bool IsDirectBuy => Steps.Count == 0;

    public string SourceLabel => SourceEnchant == 0 ? $"T{SourceTier}" : $"T{SourceTier}.{SourceEnchant}";
}

public class RefiningGrid
{
    public RawResourceType ResourceType { get; set; }
    public List<RefiningNode> Nodes { get; set; } = new(); // all 7×5 nodes (T2-T8 × .0-.4)
    public RefiningNode? GetNode(int tier, int enchant) =>
        Nodes.FirstOrDefault(n => n.Tier == tier && n.Enchant == enchant);
}