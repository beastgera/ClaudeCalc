using CraftingCalc.Models;

namespace CraftingCalc.Services;

/// <summary>
/// Computes the cost of producing a target refined material by chain-refining
/// from a user-selected starting tier. Tiers below ChainFromTier are bought
/// from market (refined). At and above ChainFromTier, each step combines:
///
///   inputs_cost      = raw_count × raw_market + lower_count × lower_unit_cost
///   effective_units  = 1 / (1 − rrr)          // expected output per refine
///   cost_per_unit    = inputs_cost / effective_units + station_fee + focus_cost
///
/// RRR stacks additively: base 15.2% + royal (if city matches) + focus + daily bonus.
/// T4's lower input is always T3 base refined (no enchanted T3 in-game) and
/// is sourced from the grid's separate T3RefinedMarketPrice field.
/// </summary>
public static class MaterialRefiningCalculator
{
    public static MaterialRefiningGrid BuildGrid(RawResourceType type)
    {
        var grid = new MaterialRefiningGrid
        {
            ResourceType = type,
            T3RefinedApiId = MaterialRefiningDatabase.GetRefinedApiId(type, 3, 0),
        };

        foreach (var (t, e) in MaterialRefiningDatabase.AllNodes())
        {
            grid.Nodes.Add(new MaterialRefiningNode
            {
                Tier = t,
                Enchant = e,
                RefinedApiId = MaterialRefiningDatabase.GetRefinedApiId(type, t, e),
                RawApiId = RawResourceDatabase.GetApiId(type, t, e),
            });
        }
        return grid;
    }

    public static MaterialRefiningResult Calculate(MaterialRefiningGrid grid, MaterialRefiningSettings settings)
    {
        var lpb = MaterialRefiningDatabase.ComputeLpb(settings);
        var rrr = MaterialRefiningDatabase.LpbToReturnRate(lpb);

        // Populate transmute alternatives for every raw on the grid before anything else,
        // so ComputeChainRefineCost can read EffectiveRawUnitPrice. When the user has
        // turned transmute Off we clear all transmute state instead so cached toggles
        // from a previous session don't keep influencing the chain.
        if (settings.TransmuteMode == TransmuteMode.On)
            ComputeRawTransmutePaths(grid, settings);
        else
            ResetRawTransmuteState(grid);

        // Reset all node compute state. Compute the grid-wide chain-refine cost for
        // every node (regardless of ChainFromTier) so the grid view can show what
        // each cell would cost — using market as the lower input where the tier is
        // below ChainFromTier, and using the just-computed chain cost otherwise.
        foreach (var node in grid.Nodes)
        {
            node.ChainRefineUnitCost = 0;
            node.HasChainCost = false;
            node.EffectiveUnitCost = 0;
            node.EffectiveFromMarket = false;
        }

        // Iterate tiers ascending so lower-tier costs are ready when needed.
        for (var t = MaterialRefiningDatabase.MinTier; t <= MaterialRefiningDatabase.MaxTier; t++)
        {
            for (var e = MaterialRefiningDatabase.MinEnchant; e <= MaterialRefiningDatabase.MaxEnchant; e++)
            {
                var node = grid.GetNode(t, e);
                if (node == null) continue;

                // 1. Always compute its chain-refine cost (for the grid view)
                node.ChainRefineUnitCost = ComputeChainRefineCost(grid, settings, t, e, lpb);
                node.HasChainCost = node.ChainRefineUnitCost > 0;

                // 2. Effective cost: depends on chain settings
                var tierIsChainRefined = t >= settings.ChainFromTier;
                if (tierIsChainRefined && node.HasChainCost)
                {
                    node.EffectiveUnitCost = node.ChainRefineUnitCost;
                    node.EffectiveFromMarket = false;
                }
                else if (node.HasRefinedMarket)
                {
                    node.EffectiveUnitCost = node.RefinedMarketPrice;
                    node.EffectiveFromMarket = true;
                }
                else
                {
                    node.EffectiveUnitCost = 0;
                }
            }
        }

        // Build the result for the target node
        return BuildResult(grid, settings, rrr);
    }

    private static decimal ComputeChainRefineCost(
        MaterialRefiningGrid grid,
        MaterialRefiningSettings settings,
        int tier,
        int enchant,
        decimal lpb)
    {
        var rawCount = MaterialRefiningDatabase.GetRawCount(tier);
        if (rawCount == 0) return 0;

        var node = grid.GetNode(tier, enchant);
        if (node == null) return 0;
        // Either direct raw market or a transmute alternative must be available.
        if (!node.HasRawMarket && !(node.UseRawTransmute && node.HasRawTransmute)) return 0;

        // Lower-tier refined input
        var lowerTier = tier - 1;
        var lowerEnchant = MaterialRefiningDatabase.GetLowerEnchant(tier, enchant);
        var lowerCount = MaterialRefiningDatabase.GetLowerRefinedCount(tier);

        decimal lowerUnitPrice;
        if (lowerTier < MaterialRefiningDatabase.MinTier)
        {
            // T4's lower input is T3 — always market
            lowerUnitPrice = grid.T3RefinedMarketPrice;
        }
        else
        {
            var lowerNode = grid.GetNode(lowerTier, lowerEnchant);
            if (lowerNode == null) return 0;

            // If lowerTier is at/above ChainFromTier, use its already-computed chain refine cost
            // (it's set during ascending iteration). Otherwise use market price.
            var lowerTierIsChain = lowerTier >= settings.ChainFromTier;
            if (lowerTierIsChain && lowerNode.HasChainCost)
                lowerUnitPrice = lowerNode.ChainRefineUnitCost;
            else
                lowerUnitPrice = lowerNode.RefinedMarketPrice;
        }

        if (lowerCount > 0 && lowerUnitPrice <= 0) return 0;

        var rawUnitPrice = node.EffectiveRawUnitPrice;
        if (rawUnitPrice <= 0) return 0;

        // Heart substitution: replace 1 raw with 1 heart per output. Not for .4 enchant.
        // Auto compares against the actual raw price being used (transmute or market).
        var heartActive = IsHeartActive(settings, enchant, rawUnitPrice);
        var effectiveRawCount = heartActive ? rawCount - 1 : rawCount;
        var heartCount = heartActive ? 1 : 0;

        var inputsCost = effectiveRawCount * (decimal)rawUnitPrice
                       + heartCount * (decimal)settings.HeartPrice
                       + lowerCount * lowerUnitPrice;
        // LPB model: expected output per refine attempt = 1 + LPB
        var effectiveUnits = 1m + lpb;
        var perUnitInputs = inputsCost / effectiveUnits;

        var station = MaterialRefiningDatabase.GetStationFeePerOutput(tier, enchant, settings.StationFeePer100Nutrition);

        return perUnitInputs + station;
    }

    private static MaterialRefiningResult BuildResult(
        MaterialRefiningGrid grid,
        MaterialRefiningSettings settings,
        decimal rrr)
    {
        var result = new MaterialRefiningResult();
        var target = grid.GetNode(settings.TargetTier, settings.TargetEnchant);
        if (target == null) return result;

        if (target.EffectiveUnitCost <= 0)
        {
            // No path — either no chain refining and no market, or chain refining
            // missing required raw/lower-tier inputs.
            return result;
        }

        result.Found = true;
        result.UnitCost = target.EffectiveUnitCost;
        result.TotalCost = result.UnitCost * settings.Quantity;
        result.DirectBuy = false;

        if (target.HasRefinedMarket)
        {
            result.SellPriceUnit = target.RefinedMarketPrice;
            result.ProfitPerUnit = target.RefinedMarketPrice - result.UnitCost;
            result.TotalProfit = result.ProfitPerUnit * settings.Quantity;
            result.RoiPercent = result.UnitCost > 0
                ? result.ProfitPerUnit / result.UnitCost * 100m
                : 0m;

            result.DirectBuyUnitPrice = target.RefinedMarketPrice;
            result.SavingsVsDirect = Math.Max(0m, (target.RefinedMarketPrice - result.UnitCost) * settings.Quantity);
        }

        // Build per-step breakdown for the chain
        var start = Math.Max(settings.ChainFromTier, MaterialRefiningDatabase.MinTier);
        for (var t = start; t <= settings.TargetTier; t++)
        {
            var step = BuildStep(grid, settings, t, settings.TargetEnchant, rrr);
            if (step != null) result.Steps.Add(step);
        }

        return result;
    }

    private static bool IsHeartActive(MaterialRefiningSettings settings, int enchant, long rawMarketPrice)
    {
        if (!MaterialRefiningDatabase.CanUseHeart(enchant)) return false;
        return settings.HeartMode switch
        {
            HeartMode.On   => true,
            HeartMode.Auto => settings.HeartPrice > 0 && settings.HeartPrice < rawMarketPrice,
            _              => false,
        };
    }

    private static void ResetRawTransmuteState(MaterialRefiningGrid grid)
    {
        foreach (var n in grid.Nodes)
        {
            n.RawTransmuteUnitCost = 0;
            n.RawTransmuteSourceTier = -1;
            n.RawTransmuteSourceEnchant = -1;
            n.RawTransmuteSourcePrice = 0;
            n.RawTransmuteSteps = new();
            n.UseRawTransmute = false;
            n.RawTransmuteManual = false;
        }
    }

    /// <summary>
    /// Runs the transmute DP over the raws of this resource type and populates
    /// each MaterialRefiningNode with the cheapest transmute path (if any).
    /// Auto-toggles UseRawTransmute whenever the user has not manually overridden
    /// it for that node: transmute is selected when strictly cheaper than direct,
    /// or when no direct market price is set.
    /// </summary>
    private static void ComputeRawTransmutePaths(MaterialRefiningGrid grid, MaterialRefiningSettings settings)
    {
        // Build a parallel RefiningGrid mirroring the raws so we can reuse the
        // existing DP without duplicating it.
        var rawGrid = new RefiningGrid { ResourceType = grid.ResourceType };
        foreach (var matNode in grid.Nodes)
        {
            rawGrid.Nodes.Add(new RefiningNode
            {
                Tier = matNode.Tier,
                Enchant = matNode.Enchant,
                ApiId = matNode.RawApiId,
                MarketPrice = matNode.RawMarketPrice,
                ManualPrice = matNode.RawManualPrice,
            });
        }

        var rawSettings = new RefiningSettings
        {
            ResourceType = grid.ResourceType,
            TargetTier = MaterialRefiningDatabase.MinTier,
            TargetEnchant = 0,
            City = settings.City,
            GlobalDiscount = settings.GlobalDiscount,
            StationFeePer100Nutrition = settings.StationFeePer100Nutrition,
        };
        // We only care about side-effects on rawGrid (CheapestUnitCost etc.).
        RefiningCalculator.Calculate(rawGrid, rawSettings);

        foreach (var matNode in grid.Nodes)
        {
            var rn = rawGrid.GetNode(matNode.Tier, matNode.Enchant);
            if (rn == null) continue;

            matNode.RawTransmuteSteps = new();
            matNode.RawTransmuteSourceTier = -1;
            matNode.RawTransmuteSourceEnchant = -1;
            matNode.RawTransmuteSourcePrice = 0;
            matNode.RawTransmuteUnitCost = 0;

            if (rn.HasCheapest && !rn.CheapestIsDirectBuy)
            {
                matNode.RawTransmuteUnitCost = rn.CheapestUnitCost;
                TraceRawTransmutePath(matNode, rawGrid, rn, rawSettings);
            }

            // Auto-decide UseRawTransmute when not manually toggled by the user.
            if (!matNode.RawTransmuteManual)
            {
                if (!matNode.HasRawTransmute)
                    matNode.UseRawTransmute = false;
                else if (matNode.RawMarketPrice <= 0)
                    matNode.UseRawTransmute = true;
                else
                    matNode.UseRawTransmute = matNode.RawTransmuteUnitCost < matNode.RawMarketPrice;
            }
        }
    }

    private static void TraceRawTransmutePath(
        MaterialRefiningNode matNode,
        RefiningGrid rawGrid,
        RefiningNode target,
        RefiningSettings rawSettings)
    {
        // Walk back through CheapestFrom* pointers until we reach a direct-buy origin.
        var chain = new List<RefiningNode> { target };
        var cur = target;
        while (!cur.CheapestIsDirectBuy && cur.CheapestFromTier != -1)
        {
            var prev = rawGrid.GetNode(cur.CheapestFromTier, cur.CheapestFromEnchant);
            if (prev == null) break;
            chain.Add(prev);
            cur = prev;
        }
        chain.Reverse();

        var source = chain[0];
        matNode.RawTransmuteSourceTier = source.Tier;
        matNode.RawTransmuteSourceEnchant = source.Enchant;
        matNode.RawTransmuteSourcePrice = source.CheapestUnitCost;

        var running = source.CheapestUnitCost;
        for (var i = 1; i < chain.Count; i++)
        {
            var from = chain[i - 1];
            var to = chain[i];
            var op = from.Tier != to.Tier ? TransmuteOp.TierUp : TransmuteOp.EnchantUp;
            var silver  = TransmuteFeeDatabase.GetEffectiveSilverFee(op, to.Tier, to.Enchant, rawSettings.GlobalDiscount);
            var station = TransmuteFeeDatabase.GetStationFee(to.Tier, to.Enchant, rawSettings.StationFeePer100Nutrition);
            running += silver + station;
            matNode.RawTransmuteSteps.Add(new TransmuteStep
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
    }

    private static MaterialRefiningStep? BuildStep(
        MaterialRefiningGrid grid,
        MaterialRefiningSettings settings,
        int tier,
        int enchant,
        decimal rrr)
    {
        var node = grid.GetNode(tier, enchant);
        if (node == null) return null;

        var rawCount = MaterialRefiningDatabase.GetRawCount(tier);
        var lowerCount = MaterialRefiningDatabase.GetLowerRefinedCount(tier);
        var lowerTier = tier - 1;
        var lowerEnchant = MaterialRefiningDatabase.GetLowerEnchant(tier, enchant);

        long lowerUnitPrice;
        bool lowerFromMarket;
        if (lowerTier < MaterialRefiningDatabase.MinTier)
        {
            lowerUnitPrice = grid.T3RefinedMarketPrice;
            lowerFromMarket = true;
        }
        else
        {
            var lowerNode = grid.GetNode(lowerTier, lowerEnchant);
            var lowerTierIsChain = lowerTier >= settings.ChainFromTier;
            if (lowerNode == null)
            {
                lowerUnitPrice = 0;
                lowerFromMarket = true;
            }
            else if (lowerTierIsChain && lowerNode.HasChainCost)
            {
                lowerUnitPrice = (long)Math.Round(lowerNode.ChainRefineUnitCost);
                lowerFromMarket = false;
            }
            else
            {
                lowerUnitPrice = lowerNode.RefinedMarketPrice;
                lowerFromMarket = true;
            }
        }

        var station = MaterialRefiningDatabase.GetStationFeePerOutput(tier, enchant, settings.StationFeePer100Nutrition);

        var rawUnitPrice = node.EffectiveRawUnitPrice;
        var heartActive = IsHeartActive(settings, enchant, rawUnitPrice);
        var displayRawCount = heartActive ? rawCount - 1 : rawCount;
        var heartCount = heartActive ? 1 : 0;

        var rawFromTransmute = node.UseRawTransmute && node.HasRawTransmute;

        return new MaterialRefiningStep
        {
            Tier = tier,
            Enchant = enchant,
            RawCount = displayRawCount,
            RawUnitPrice = rawUnitPrice,
            HeartCount = heartCount,
            HeartUnitPrice = heartActive ? settings.HeartPrice : 0,
            LowerCount = lowerCount,
            LowerUnitPrice = lowerUnitPrice,
            LowerTier = lowerTier,
            LowerEnchant = lowerEnchant,
            LowerFromMarket = lowerFromMarket,
            ReturnRate = rrr,
            StationFeePerOutputUnit = station,
            CostPerOutputUnit = node.ChainRefineUnitCost,
            RawFromTransmute = rawFromTransmute,
            RawTransmuteSourceTier = rawFromTransmute ? node.RawTransmuteSourceTier : -1,
            RawTransmuteSourceEnchant = rawFromTransmute ? node.RawTransmuteSourceEnchant : -1,
            RawTransmuteSourcePrice = rawFromTransmute ? node.RawTransmuteSourcePrice : 0,
            RawTransmuteSteps = rawFromTransmute ? node.RawTransmuteSteps : new(),
        };
    }
}