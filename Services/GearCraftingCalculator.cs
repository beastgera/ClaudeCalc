using CraftingCalc.Models;

namespace CraftingCalc.Services;

public static class GearCraftingCalculator
{
    private static readonly int[] Tiers = [4, 5, 6, 7, 8];
    private static readonly int[] Enchants = [0, 1, 2, 3, 4];

    public static List<CraftingRow> BuildRows(GearItem item)
    {
        var rows = new List<CraftingRow>();
        foreach (var tier in Tiers)
        foreach (var enchant in Enchants)
        {
            rows.Add(new CraftingRow
            {
                Tier = tier,
                Enchant = enchant,
                ItemApiId = GearItemDatabase.GetItemApiId(item, tier, enchant)
            });
        }
        return rows;
    }

    public static void Calculate(CraftingRow row, GearItem item, CraftingSettings s)
    {
        var tierMult = GearItemDatabase.TierQtyMultiplier[row.Tier];

        var mat1BaseQty = item.Mat1Qty;
        var mat2BaseQty = item.Mat2Qty;

        // Convert RRR → LPB, add bonus day (flat LPB), then compute effective cost.
        // Formula: LPB = RRR / (1 - RRR)  →  effectiveCost = rawCost / (1 + LPB) = rawCost * (1 - RRR)
        var effectiveLpb = (s.IsHideout ? 0m : s.BaseLocalProductionBonus) + s.BonusDay
                               + s.LocationQualityBonus
                               + s.HideoutPowerBonus
                               + (s.UseFocus ? s.FocusLpb : 0m);

        // Material cost before return rate
        var rawMatCost = row.Mat1Price * mat1BaseQty + row.Mat2Price * mat2BaseQty;

        // Effective material cost after crafting return (net spend after mats returned)
        var effectiveMatCost = rawMatCost / (1m + effectiveLpb);

        // Artifact cost (not subject to return rate — consumed fully)
        var artifactCost = row.ArtifactPrice * item.ArtifactQty
                         + row.Artifact2Price * item.Artifact2Qty;

        row.TotalCost = (effectiveMatCost + artifactCost) * s.Quantity;

        // Revenue after market tax
        var revenue = row.SellPrice * (1m - s.MarketTax) * s.Quantity;

        row.Profit = revenue - row.TotalCost;

        // Fame per craft
        var fameMult = GearItemDatabase.GetFameMultiplier(row.Tier, row.Enchant);
        row.Fame = (item.Mat1Qty + item.Mat2Qty) * tierMult * fameMult * s.Quantity;

        row.Gain = row.TotalCost > 0 ? row.Profit / row.TotalCost : 0;

        // Net mats consumed (purchased qty minus what gets returned)
        row.Mat1Required = (int)Math.Ceiling(mat1BaseQty * s.Quantity / (1m + effectiveLpb));
        row.Mat2Required = (int)Math.Ceiling(mat2BaseQty * s.Quantity / (1m + effectiveLpb));
    }

    public static void RecalculateAll(List<CraftingRow> rows, GearItem item, CraftingSettings settings)
    {
        foreach (var row in rows)
            Calculate(row, item, settings);
    }

    /// <summary>Returns all item IDs needed for API price fetch (materials + item).</summary>
    public static (HashSet<string> MatIds, HashSet<string> ItemIds, HashSet<string> ArtifactIds)
        GetRequiredApiIds(GearItem item)
    {
        var matIds = new HashSet<string>();
        var itemIds = new HashSet<string>();
        var artifactIds = new HashSet<string>();

        foreach (var tier in new[] { 4, 5, 6, 7, 8 })
        {
            var artId = GearItemDatabase.GetArtifactApiId(item, tier);
            if (artId != null) artifactIds.Add(artId);

            var art2Id = GearItemDatabase.GetArtifact2ApiId(item, tier);
            if (art2Id != null) artifactIds.Add(art2Id);

            foreach (var enchant in new[] { 0, 1, 2, 3, 4 })
            {
                itemIds.Add(GearItemDatabase.GetItemApiId(item, tier, enchant));
                matIds.Add(GearItemDatabase.GetMatApiId(item.Mat1Type, tier, enchant));
                if (!string.IsNullOrEmpty(item.Mat2Type))
                    matIds.Add(GearItemDatabase.GetMatApiId(item.Mat2Type, tier, enchant));
            }

        }

        return (matIds, itemIds, artifactIds);
    }
}