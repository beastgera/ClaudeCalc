namespace CraftingCalc.Services;

/// <summary>
/// Transmute economics per destination node (tier, enchant):
///   - BaseFees: silver fee charged by transmute, scaled by global discount.
///   - Nutrition: station nutrition consumed per transmute; total station fee
///                = nutrition × (station_fee_per_100_nutrition / 100).
/// Same values apply whether the source is a lower tier or a lower enchant.
/// T2/T3 are non-transmutable so their fees are 0 (nutrition values still
/// stored for reference, only .0 levels exist).
/// </summary>
public static class TransmuteFeeDatabase
{
    // Indexed by [tier - 2, enchant]. Tier 2..8, enchant 0..4.
    private static readonly long[,] BaseFees =
    {
        // T2 — not transmutable
        { 0,       0,       0,       0,       0       },
        // T3 — not transmutable
        { 0,       0,       0,       0,       0       },
        // T4 — base, .1, .2, .3, .4
        { 0,       1_500,   3_000,   6_000,   24_000  },
        // T5
        { 781,     2_000,   4_000,   8_000,   32_000  },
        // T6
        { 1_250,   3_000,   6_000,   19_800,  79_200  },
        // T7
        { 2_500,   4_800,   15_120,  49_896,  199_584 },
        // T8
        { 5_000,   14_400,  45_360,  149_688, 779_625 },
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

    public static long GetBaseFee(int tier, int enchant)
    {
        if (!InRange(tier, enchant)) return 0;
        return BaseFees[tier - 2, enchant];
    }

    public static decimal GetNutrition(int tier, int enchant)
    {
        if (!InRange(tier, enchant)) return 0m;
        return NutritionTable[tier - 2, enchant];
    }

    /// <summary>Silver fee after the global discount (negative discount = surcharge).</summary>
    public static long GetEffectiveSilverFee(int tier, int enchant, decimal globalDiscount)
    {
        var baseFee = GetBaseFee(tier, enchant);
        if (baseFee == 0) return 0;
        var multiplier = Math.Max(0m, 1m - globalDiscount);
        return (long)Math.Round(baseFee * multiplier);
    }

    /// <summary>Station fee = nutrition × (feePer100Nutrition / 100).</summary>
    public static long GetStationFee(int tier, int enchant, decimal stationFeePer100Nutrition)
    {
        if (stationFeePer100Nutrition <= 0m) return 0;
        var nutrition = GetNutrition(tier, enchant);
        if (nutrition <= 0m) return 0;
        var fee = nutrition * stationFeePer100Nutrition / 100m;
        return fee <= 0m ? 0 : (long)Math.Round(fee);
    }

    /// <summary>Total per-unit transmute cost: silver + station.</summary>
    public static long GetTotalFee(int tier, int enchant, decimal globalDiscount, decimal stationFeePer100Nutrition)
    {
        var silver  = GetEffectiveSilverFee(tier, enchant, globalDiscount);
        var station = GetStationFee(tier, enchant, stationFeePer100Nutrition);
        // Guard overflow on sum (extremely unlikely with realistic inputs)
        if (silver >= long.MaxValue - station) return long.MaxValue;
        return silver + station;
    }

    private static bool InRange(int tier, int enchant) =>
        tier is >= 2 and <= 8 && enchant is >= 0 and <= 4;
}