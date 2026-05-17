using CraftingCalc.Models;

namespace CraftingCalc.Services;

public static class FoodCraftingCalculator
{
    public static FoodCraftingRow BuildRow(FoodItem item) => new() { ItemApiId = item.ApiId };

    public static void Calculate(FoodCraftingRow row, FoodItem item, CraftingSettings s)
    {
        var effectiveLpb = (s.IsHideout ? 0m : s.BaseLocalProductionBonus)
                         + s.BonusDay
                         + s.LocationQualityBonus
                         + s.HideoutPowerBonus
                         + s.BonusCityBonus
                         + (s.UseFocus ? s.FocusLpb : 0m);

        var rawMatCost = row.Mat1Price * item.Mat1.Qty
                       + row.Mat2Price * (item.Mat2?.Qty ?? 0)
                       + row.Mat3Price * (item.Mat3?.Qty ?? 0)
                       + row.Mat4Price * (item.Mat4?.Qty ?? 0)
                       + row.SaucePrice * (item.RequiredSauce?.Qty ?? 0);

        var effectiveMatCost = rawMatCost / (1m + effectiveLpb);
        row.TotalCost = effectiveMatCost * s.Quantity;

        var revenue = row.SellPrice * (1m - s.MarketTax) * item.OutputQty * s.Quantity;
        row.Profit = revenue - row.TotalCost;
        row.Gain = row.TotalCost > 0 ? row.Profit / row.TotalCost : 0;

        row.Mat1Required  = (int)Math.Ceiling(item.Mat1.Qty * s.Quantity / (1m + effectiveLpb));
        row.Mat2Required  = item.Mat2 is null ? 0 : (int)Math.Ceiling(item.Mat2.Qty * s.Quantity / (1m + effectiveLpb));
        row.Mat3Required  = item.Mat3 is null ? 0 : (int)Math.Ceiling(item.Mat3.Qty * s.Quantity / (1m + effectiveLpb));
        row.Mat4Required  = item.Mat4 is null ? 0 : (int)Math.Ceiling(item.Mat4.Qty * s.Quantity / (1m + effectiveLpb));
        row.SauceRequired = item.RequiredSauce is null ? 0 : (int)Math.Ceiling(item.RequiredSauce.Qty * s.Quantity / (1m + effectiveLpb));
    }

    public static (HashSet<string> MatIds, HashSet<string> ItemIds, HashSet<string> ArtifactIds)
        GetRequiredApiIds(FoodItem item)
    {
        var matIds = new HashSet<string> { item.Mat1.ApiId };
        if (item.Mat2 != null) matIds.Add(item.Mat2.ApiId);
        if (item.Mat3 != null) matIds.Add(item.Mat3.ApiId);
        if (item.Mat4 != null) matIds.Add(item.Mat4.ApiId);
        if (item.RequiredSauce != null) matIds.Add(item.RequiredSauce.ApiId);
        return (matIds, [item.ApiId], []);
    }
}