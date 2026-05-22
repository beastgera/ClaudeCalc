namespace CraftingCalc.Services;

/// <summary>Which transmute operation reaches a destination node.</summary>
public enum TransmuteOp
{
    /// <summary>(t-1, e) → (t, e): raise the tier, keep the enchant.</summary>
    TierUp,
    /// <summary>(t, e-1) → (t, e): raise the enchant, keep the tier.</summary>
    EnchantUp,
}

/// <summary>
/// Transmute economics per destination node (tier, enchant) and operation.
///
/// The silver fee a transmute charges depends on BOTH the destination node and
/// how you reached it:
///   - EnchantUpFees: enchant-up transmutes  (t, e-1) → (t, e).
///   - TierUpFees:    tier-up transmutes     (t-1, e) → (t, e).
/// The two tables agree only on the unenchanted column (.0), which can only be
/// reached by tier-up; for .1-.4 the two operations charge different amounts
/// (tier-up is not uniformly cheaper — e.g. it costs more at T7/T8 enchant 1).
///
/// Station nutrition is the same for either operation onto a given node, so it
/// stays in a single table; total station fee = nutrition ×
/// (station_fee_per_100_nutrition / 100).
///
/// T2/T3 are non-transmutable so their fees are 0 (nutrition values still
/// stored for reference, only .0 levels exist). The lowest tier-up destination
/// is T5 — a tier-up needs a T4+ source — so T2-T4 rows of TierUpFees are 0.
/// </summary>
public static class TransmuteFeeDatabase
{
    // Enchant-up fee INTO (tier, enchant). Indexed [tier - 2, enchant].
    // Column 0 is unused — there is no enchant-up onto an unenchanted node.
    private static readonly long[,] EnchantUpFees =
    {
        // T2 — not transmutable
        { 0, 0,       0,       0,       0       },
        // T3 — not transmutable
        { 0, 0,       0,       0,       0       },
        // T4 — base, .1, .2, .3, .4
        { 0, 1_500,   3_000,   6_000,   24_000  },
        // T5
        { 0, 2_000,   4_000,   8_000,   32_000  },
        // T6
        { 0, 3_000,   6_000,   19_800,  79_200  },
        // T7
        { 0, 4_800,   15_120,  49_896,  199_584 },
        // T8
        { 0, 14_400,  45_360,  149_688, 784_440 },
    };

    // Tier-up fee INTO (tier, enchant). Indexed [tier - 2, enchant].
    // T2-T4 rows are 0: the lowest tier-up destination is T5.
    private static readonly long[,] TierUpFees =
    {
        // T2 — not a tier-up destination
        { 0,     0,       0,       0,        0       },
        // T3 — not a tier-up destination
        { 0,     0,       0,       0,        0       },
        // T4 — not a tier-up destination
        { 0,     0,       0,       0,        0       },
        // T5 — base, .1, .2, .3, .4
        { 781,   1_563,   3_125,   6_250,    25_000  },
        // T6
        { 1_250, 2_500,   5_000,   16_500,   66_000  },
        // T7
        { 2_500, 5_000,   15_750,  51_975,   207_900 },
        // T8
        { 5_000, 15_000,  47_250,  155_925,  779_625 },
    };

    // Nutrition consumed per transmute. T2.1+ and T3.1+ don't exist (raw resources
    // below T4 have no enchanted variants), so they're stored as 0.
    private static readonly decimal[,] NutritionTable =
    {
        // T2
        { 0.1125m, 0m,       0m,       0m,       0m       },
        // T3
        { 0.225m,  0m,       0m,       0m,       0m       },
        // T4
        { 0.45m,   1.35m,    3.15m,    6.75m,    13.95m   },
        // T5
        { 0.6007m, 1.1992m,  2.4007m,  4.7992m,  9.5625m  },
        // T6
        { 0.9m,    1.8m,     3.6m,     7.2m,     14.4m    },
        // T7
        { 1.44m,   2.88m,    5.76m,    11.52m,   22.95m   },
        // T8
        { 2.88m,   5.76m,    11.52m,   23.13m,   46.0125m },
    };

    /// <summary>Raw silver fee for transmuting <paramref name="op"/> into (tier, enchant).</summary>
    public static long GetBaseFee(TransmuteOp op, int tier, int enchant)
    {
        if (!InRange(tier, enchant)) return 0;
        var table = op == TransmuteOp.TierUp ? TierUpFees : EnchantUpFees;
        return table[tier - 2, enchant];
    }

    public static decimal GetNutrition(int tier, int enchant)
    {
        if (!InRange(tier, enchant)) return 0m;
        return NutritionTable[tier - 2, enchant];
    }

    /// <summary>Silver fee after the global discount (negative discount = surcharge).</summary>
    public static long GetEffectiveSilverFee(TransmuteOp op, int tier, int enchant, decimal globalDiscount)
    {
        var baseFee = GetBaseFee(op, tier, enchant);
        if (baseFee == 0) return 0;
        var multiplier = Math.Max(0m, 1m - globalDiscount);
        return (long)Math.Round(baseFee * multiplier);
    }

    /// <summary>Station fee = nutrition × (feePer100Nutrition / 100). Same for either operation.</summary>
    public static long GetStationFee(int tier, int enchant, decimal stationFeePer100Nutrition)
    {
        if (stationFeePer100Nutrition <= 0m) return 0;
        var nutrition = GetNutrition(tier, enchant);
        if (nutrition <= 0m) return 0;
        var fee = nutrition * stationFeePer100Nutrition / 100m;
        return fee <= 0m ? 0 : (long)Math.Round(fee);
    }

    /// <summary>Total per-unit transmute cost: silver + station.</summary>
    public static long GetTotalFee(TransmuteOp op, int tier, int enchant, decimal globalDiscount, decimal stationFeePer100Nutrition)
    {
        var silver  = GetEffectiveSilverFee(op, tier, enchant, globalDiscount);
        var station = GetStationFee(tier, enchant, stationFeePer100Nutrition);
        // Guard overflow on sum (extremely unlikely with realistic inputs)
        if (silver >= long.MaxValue - station) return long.MaxValue;
        return silver + station;
    }

    private static bool InRange(int tier, int enchant) =>
        tier is >= 2 and <= 8 && enchant is >= 0 and <= 4;
}