namespace CraftingCalc.Models;

public class CartEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ItemName { get; set; } = "";
    public string ItemApiId { get; set; } = "";
    public int Tier { get; set; }
    public int Enchant { get; set; }
    public int Quantity { get; set; }
    public string TierLabel => $"T{Tier}.{Enchant}";
    public List<CartMaterial> Materials { get; set; } = [];
}


public class CartMaterial
{
    public string ApiId { get; set; } = "";
    public string Name { get; set; } = "";
    public int Required { get; set; }
    public long Price { get; set; }   // price per unit at time of adding to cart
}
