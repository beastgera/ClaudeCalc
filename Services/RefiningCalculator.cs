using CraftingCalc.Models;

namespace CraftingCalc.Services;

/// <summary>
/// Finds the cheapest way to obtain a target raw resource (tier+enchant) by
/// considering direct market buy vs. a chain of 1→1 transmutes (tier-up or
/// enchant-up). Operates on a 7×5 grid (T2-T8 × .0-.4).
///
/// DP recurrence: cost[t,e] = min(
///     market[t,e],
///     cost[t-1,e] + tierUpFee[t,e],     (tier-up transmute, only if t >= 5)
///     cost[t,e-1] + enchantUpFee[t,e]   (enchant-up transmute, only if e >= 1)
/// )
/// Tier-up and enchant-up charge different fees onto the same enchanted
/// destination, so each edge is priced with its own operation.
/// Since the graph is a DAG with edges going up-and-right, evaluating in
/// (tier, enchant) ascending order is enough — no Dijkstra needed.
/// </summary>
public static class RefiningCalculator
{
    public static RefiningGrid BuildGrid(RawResourceType type)
    {
        var grid = new RefiningGrid { ResourceType = type };
        foreach (var (t, e) in RawResourceDatabase.AllNodes())
        {
            grid.Nodes.Add(new RefiningNode
            {
                Tier = t,
                Enchant = e,
                ApiId = RawResourceDatabase.GetApiId(type, t, e),
            });
        }
        return grid;
    }

    public static RefiningPath Calculate(RefiningGrid grid, RefiningSettings settings)
    {
        var forced = settings.HasForcedSource;
        var startTier = settings.StartTier ?? -1;
        var startEnchant = settings.StartEnchant ?? -1;

        // Reset DP state on every node. In forced mode, only the chosen source
        // is a direct-buy origin (price falls back to 1 silver if unset);
        // every other cell must be reached via transmute from there.
        foreach (var n in grid.Nodes)
        {
            if (forced)
            {
                if (n.Tier == startTier && n.Enchant == startEnchant)
                {
                    n.CheapestUnitCost = n.MarketPrice > 0 ? n.MarketPrice : 1;
                    n.CheapestIsDirectBuy = true;
                }
                else
                {
                    n.CheapestUnitCost = long.MaxValue;
                    n.CheapestIsDirectBuy = false;
                }
            }
            else
            {
                n.CheapestUnitCost = n.HasMarketPrice ? n.MarketPrice : long.MaxValue;
                n.CheapestIsDirectBuy = n.HasMarketPrice;
            }
            n.CheapestFromTier = -1;
            n.CheapestFromEnchant = -1;
        }

        // DP — iterate tiers ascending, then enchants ascending
        for (var t = RawResourceDatabase.MinTier; t <= RawResourceDatabase.MaxTier; t++)
        {
            for (var e = RawResourceDatabase.MinEnchant; e <= RawResourceDatabase.MaxEnchant; e++)
            {
                var node = grid.GetNode(t, e);
                if (node == null) continue;

                // Forced source is locked above and can't be overwritten.
                if (forced && t == startTier && e == startEnchant) continue;

                // Auto mode: manual prices win — never overwrite them with a cheaper transmute path.
                // The cell still serves as a source for downstream transmute candidates.
                if (!forced && node.ManualPrice) continue;

                // Tier-up: source = (t-1, e), only valid when (t-1) >= TransmuteMinTier
                if (t - 1 >= RawResourceDatabase.TransmuteMinTier)
                {
                    var src = grid.GetNode(t - 1, e);
                    if (src is { CheapestUnitCost: < long.MaxValue })
                    {
                        var fee = TransmuteFeeDatabase.GetTotalFee(
                            TransmuteOp.TierUp, t, e, settings.GlobalDiscount, settings.StationFeePer100Nutrition);
                        var candidate = SafeAdd(src.CheapestUnitCost, fee);
                        if (candidate < node.CheapestUnitCost)
                        {
                            node.CheapestUnitCost = candidate;
                            node.CheapestIsDirectBuy = false;
                            node.CheapestFromTier = t - 1;
                            node.CheapestFromEnchant = e;
                        }
                    }
                }

                // Enchant-up: source = (t, e-1)
                if (e - 1 >= RawResourceDatabase.MinEnchant)
                {
                    var src = grid.GetNode(t, e - 1);
                    if (src is { CheapestUnitCost: < long.MaxValue })
                    {
                        var fee = TransmuteFeeDatabase.GetTotalFee(
                            TransmuteOp.EnchantUp, t, e, settings.GlobalDiscount, settings.StationFeePer100Nutrition);
                        var candidate = SafeAdd(src.CheapestUnitCost, fee);
                        if (candidate < node.CheapestUnitCost)
                        {
                            node.CheapestUnitCost = candidate;
                            node.CheapestIsDirectBuy = false;
                            node.CheapestFromTier = t;
                            node.CheapestFromEnchant = e - 1;
                        }
                    }
                }
            }
        }

        return BuildPath(grid, settings);
    }

    private static RefiningPath BuildPath(RefiningGrid grid, RefiningSettings settings)
    {
        var path = new RefiningPath();
        var target = grid.GetNode(settings.TargetTier, settings.TargetEnchant);
        if (target == null || !target.HasCheapest) return path;

        path.Found = true;
        path.UnitCost = target.CheapestUnitCost;
        path.TotalCost = SafeMul(path.UnitCost, settings.Quantity);

        if (target.HasMarketPrice)
        {
            path.DirectBuyUnitCost = target.MarketPrice;
            path.DirectBuyTotalCost = SafeMul(target.MarketPrice, settings.Quantity);
            path.Savings = Math.Max(0, path.DirectBuyTotalCost - path.TotalCost);
        }

        // Trace back from target through CheapestFrom* pointers until we hit a direct-buy node
        var chain = new List<RefiningNode> { target };
        var cur = target;
        while (!cur.CheapestIsDirectBuy && cur.CheapestFromTier != -1)
        {
            var prev = grid.GetNode(cur.CheapestFromTier, cur.CheapestFromEnchant);
            if (prev == null) break;
            chain.Add(prev);
            cur = prev;
        }
        chain.Reverse();

        var source = chain[0];
        path.SourceTier = source.Tier;
        path.SourceEnchant = source.Enchant;
        path.SourceApiId = source.ApiId;
        // Use CheapestUnitCost so the forced-source fallback (1 silver) shows up correctly.
        path.SourceUnitPrice = source.CheapestUnitCost;

        // Build steps with running cost (source price + accumulated fees)
        var running = source.CheapestUnitCost;
        for (var i = 1; i < chain.Count; i++)
        {
            var from = chain[i - 1];
            var to = chain[i];
            var op = from.Tier != to.Tier ? TransmuteOp.TierUp : TransmuteOp.EnchantUp;
            var silver  = TransmuteFeeDatabase.GetEffectiveSilverFee(op, to.Tier, to.Enchant, settings.GlobalDiscount);
            var station = TransmuteFeeDatabase.GetStationFee(to.Tier, to.Enchant, settings.StationFeePer100Nutrition);
            running = SafeAdd(running, silver + station);
            path.Steps.Add(new TransmuteStep
            {
                FromTier = from.Tier,
                FromEnchant = from.Enchant,
                ToTier = to.Tier,
                ToEnchant = to.Enchant,
                SilverFeePerUnit = silver,
                StationFeePerUnit = station,
                RunningUnitCost = running,
            });
        }

        return path;
    }

    private static long SafeAdd(long a, long b)
    {
        if (a >= long.MaxValue - b) return long.MaxValue;
        return a + b;
    }

    private static long SafeMul(long a, int b)
    {
        if (b <= 0) return 0;
        if (a >= long.MaxValue / b) return long.MaxValue;
        return a * b;
    }
}