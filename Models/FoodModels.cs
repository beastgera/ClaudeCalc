namespace CraftingCalc.Models;

public record FoodIngredient(string ApiId, string Name, int Qty);

public record FoodItem(
    string ApiId,        // full item API ID, e.g. "T7_MEAL_OMELETTE"
    string Name,
    int Tier,
    int OutputQty,       // items produced per craft (10 for standard meals, 1 for fish meals)
    FoodIngredient Mat1,
    FoodIngredient? Mat2,
    FoodIngredient? Mat3,
    FoodIngredient? Mat4,
    string BonusCity
)
{
    public int EnchantmentLevel { get; init; } = 0;
    public FoodIngredient? RequiredSauce { get; init; }
};

public class FoodCraftingRow
{
    public string ItemApiId { get; set; } = "";

    public decimal Mat1Price { get; set; }
    public bool Mat1PriceManual { get; set; }
    public decimal Mat2Price { get; set; }
    public bool Mat2PriceManual { get; set; }
    public decimal Mat3Price { get; set; }
    public bool Mat3PriceManual { get; set; }
    public decimal Mat4Price { get; set; }
    public bool Mat4PriceManual { get; set; }
    public decimal SellPrice { get; set; }
    public bool SellPriceManual { get; set; }

    public decimal TotalCost { get; set; }
    public decimal Profit { get; set; }
    public decimal Gain { get; set; }

    public int Mat1Required { get; set; }
    public int Mat2Required { get; set; }
    public int Mat3Required { get; set; }
    public int Mat4Required { get; set; }

    public decimal SaucePrice { get; set; }
    public bool SaucePriceManual { get; set; }
    public int SauceRequired { get; set; }

    public decimal ApiSellPrice { get; set; }
    public decimal ApiMat1Price { get; set; }
    public decimal ApiMat2Price { get; set; }
    public decimal ApiMat3Price { get; set; }
    public decimal ApiMat4Price { get; set; }
    public decimal ApiSaucePrice { get; set; }
    public Dictionary<string, long> ItemCityPrices { get; set; } = [];

    public DateTime? PricesLoadedAt { get; set; }
}