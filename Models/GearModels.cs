using System.Text.Json.Serialization;

namespace CraftingCalc.Models;

public record GearItem(
    string Id,
    string Name,
    int Mat1Qty,
    string Mat1Type,
    int Mat2Qty,
    string? Mat2Type,
    string BonusCity,
    string? ArtifactId,
    string? ArtifactName,
    int ArtifactQty,
    string? Artifact2Id,
    string? Artifact2Name,
    int Artifact2Qty,
    int JournalType,   // 1=Mage, 2=Hunter, 3=Warrior, 4=Royal
    int BaseFame
);

public class CraftingSettings
{
    public int Quantity { get; set; } = 1;
    public decimal MarketTax { get; set; } = 0.065m;
    public decimal BaseLocalProductionBonus { get; set; } = 0.18m;
    public int ItemQuality { get; set; } = 4;
    public string BuyArtifactCity { get; set; } = "Caerleon";
    public string SellItemCity { get; set; } = "Fort Sterling";
    public bool UseBuyOrder { get; set; } = false;
    public bool UseFocus { get; set; } = false;
    public string CraftCity { get; set; } = "Caerleon";
    public decimal FocusLpb { get; set; } = 0.59m;
    public decimal BonusDay { get; set; } = 0m;           // 0, 0.10, or 0.20 — flat LPB added (bonus day event)
    public decimal LocationQualityBonus { get; set; } = 0m; // LPB from hideout zone quality (0 if in city)
    public decimal HideoutPowerBonus { get; set; } = 0m;    // LPB from power core (0 if in city)
    public bool IsHideout { get; set; } = false;
    public decimal BonusCityBonus { get; set; } = 0m;       // LPB from crafting in bonus city (0.15)
    public bool BlackMarketMode { get; set; } = false;      // Single-craft outcome distribution
}

public class CraftingOutcome
{
    public int Mat1Qty { get; set; }
    public int Mat2Qty { get; set; }
    public decimal TotalCost { get; set; }
    public decimal Profit { get; set; }
    public decimal Probability { get; set; }
}

public class CraftingRow
{
    public int Tier { get; set; }
    public int Enchant { get; set; }
    public string TierLabel => $"T{Tier}.{Enchant}";
    public string ItemApiId { get; set; } = "";

    // Prices (can be overridden manually)
    public decimal Mat1Price { get; set; }
    public bool Mat1PriceManual { get; set; }
    public decimal Mat2Price { get; set; }
    public bool Mat2PriceManual { get; set; }
    public decimal ArtifactPrice { get; set; }
    public bool ArtifactPriceManual { get; set; }
    public decimal Artifact2Price { get; set; }
    public bool Artifact2PriceManual { get; set; }
    public decimal SellPrice { get; set; }
    public bool SellPriceManual { get; set; }

    // Calculated
    public decimal TotalCost { get; set; }
    public decimal Profit { get; set; }
    public decimal Gain { get; set; }   // e.g. 0.15 = 15%
    public decimal Fame { get; set; }
    public int Mat1Required { get; set; }
    public int Mat2Required { get; set; }

    // Focus consumption (base per craft, and total for Quantity — only when UseFocus is on)
    public int FocusPerCraft { get; set; }        // after spec reduction
    public int FocusPerCraftBase { get; set; }    // before spec reduction
    public int FocusRequired { get; set; }
    public decimal SilverPerFocus { get; set; }
    public decimal SpecReduction { get; set; }    // 0.0 to 0.475

    public decimal ApiSellPrice { get; set; }
    public decimal ApiMat1Price { get; set; }
    public decimal ApiMat2Price { get; set; }
    public decimal ApiArtifactPrice { get; set; }
    public decimal ApiArtifact2Price { get; set; }
    public Dictionary<string, long> ItemCityPrices { get; set; } = [];

    public bool IsLoading { get; set; }
    public DateTime? PricesLoadedAt { get; set; }

    public List<CraftingOutcome> Outcomes { get; set; } = [];
}

public class AlbionMarketEntry
{
    [JsonPropertyName("item_id")]
    public string ItemId { get; set; } = "";
    [JsonPropertyName("city")]
    public string City { get; set; } = "";
    [JsonPropertyName("quality")]
    public int Quality { get; set; }
    [JsonPropertyName("sell_price_min")]
    public long SellPriceMin { get; set; }
    [JsonPropertyName("sell_price_min_date")]
    public DateTime SellPriceMinDate { get; set; }
    [JsonPropertyName("sell_price_max")]
    public long SellPriceMax { get; set; }
    [JsonPropertyName("buy_price_min")]
    public long BuyPriceMin { get; set; }
    [JsonPropertyName("buy_price_max")]
    public long BuyPriceMax { get; set; }
    [JsonPropertyName("buy_price_max_date")]
    public DateTime BuyPriceMaxDate { get; set; }
}
