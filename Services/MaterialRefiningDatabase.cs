using CraftingCalc.Models;

namespace CraftingCalc.Services;

/// <summary>
/// Recipe + return-rate database for refining raw resources into refined materials
/// (Wood→Planks, Ore→Metal Bar, Hide→Leather, Fiber→Cloth).
///
/// Refining grid covers T4-T8 × .0-.4. T3 refined material is needed as an
/// input for the T4 step but is itself not refined here (no enchanted T3
/// variants exist in-game) — its market price is fetched separately.
/// </summary>
public static class MaterialRefiningDatabase
{
    public const int MinTier = 4;
    public const int MaxTier = 8;
    public const int MinEnchant = 0;
    public const int MaxEnchant = 4;
    public const int LowerTierForT4 = 3; // T4 needs T3 refined as the lower input

    // ─── Recipe (per 1 refined output unit, before return rate) ─────────────
    // T2: 1 raw                            (out of scope)
    // T3: 2 raw + 1 T2 refined             (out of scope — bought as market input only)
    // T4: 2 raw + 1 T3 refined
    // T5: 3 raw + 1 T4 refined
    // T6: 4 raw + 1 T5 refined
    // T7: 5 raw + 1 T6 refined
    // T8: 5 raw + 1 T7 refined
    public static int GetRawCount(int tier) => tier switch
    {
        4 => 2,
        5 => 3,
        6 => 4,
        7 => 5,
        8 => 5,
        _ => 0,
    };

    public static int GetLowerRefinedCount(int tier) =>
        tier is >= 4 and <= 8 ? 1 : 0;

    /// <summary>Heart recipe is not available for enchant level 4.</summary>
    public static bool CanUseHeart(int enchant) => enchant is >= 0 and <= 3;

    /// <summary>
    /// Enchant carried by the lower-tier refined input.
    /// T5+ propagates the same enchant; T4 always pulls T3 base (T3 has no enchanted variants).
    /// </summary>
    public static int GetLowerEnchant(int tier, int enchant) =>
        tier == 4 ? 0 : enchant;

    // ─── Naming ─────────────────────────────────────────────────────────────
    public static string GetRefinedCode(RawResourceType type) => type switch
    {
        RawResourceType.Wood  => "PLANKS",
        RawResourceType.Ore   => "METALBAR",
        RawResourceType.Hide  => "LEATHER",
        RawResourceType.Fiber => "CLOTH",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    public static string GetRefinedDisplayName(RawResourceType type) => type switch
    {
        RawResourceType.Wood  => "Planks",
        RawResourceType.Ore   => "Metal Bar",
        RawResourceType.Hide  => "Leather",
        RawResourceType.Fiber => "Cloth",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    public static string GetRefinedApiId(RawResourceType type, int tier, int enchant)
    {
        var code = GetRefinedCode(type);
        var suffix = enchant > 0 ? $"_LEVEL{enchant}@{enchant}" : "";
        return $"T{tier}_{code}{suffix}";
    }

    // ─── Royal city map ─────────────────────────────────────────────────────
    public static string GetRoyalCity(RawResourceType type) => type switch
    {
        RawResourceType.Wood  => "Fort Sterling",
        RawResourceType.Ore   => "Thetford",
        RawResourceType.Hide  => "Martlock",
        RawResourceType.Fiber => "Lymhurst",
        _ => "Caerleon"
    };

    public static bool IsRoyalCityForType(string city, RawResourceType type) =>
        string.Equals(city, GetRoyalCity(type), StringComparison.OrdinalIgnoreCase);

    // ─── Local Production Bonus (LPB) ───────────────────────────────────────
    // Refining uses the same LPB stack as gear/food crafting:
    //   effective_output = 1 + LPB     (per 1 unit worth of inputs you get back 1+LPB units)
    //   return_rate      = LPB / (1 + LPB)
    //   effective_cost   = raw_cost / (1 + LPB)
    //
    // LPB is additive across: base (0.18 in city) + bonus day + bonus city
    // (0.15 when refining in matching royal city) + focus (0.59).
    public static decimal ComputeLpb(MaterialRefiningSettings s)
    {
        return s.BaseLocalProductionBonus
             + s.BonusDay
             + s.BonusCityBonus
             + (s.UseFocus ? s.FocusLpb : 0m);
    }

    public static decimal LpbToReturnRate(decimal lpb) =>
        lpb <= 0m ? 0m : lpb / (1m + lpb);

    // ─── Nutrition per refined output unit ──────────────────────────────────
    // Base nutrition doubles each tier; each enchant level also doubles it.
    //   T2 = 0.225, T3 = 0.9, T4 = 1.8, T5 = 3.6, T6 = 7.2, T7 = 14.4, T8 = 28.8
    //   enchant multiplier: .0=1, .1=2, .2=4, .3=8, .4=16
    private static readonly decimal[] BaseNutritionPerTier =
    {
        0, 0,        // T0, T1
        0.225m,      // T2
        0.9m,        // T3
        1.8m,        // T4
        3.6m,        // T5
        7.2m,        // T6
        14.4m,       // T7
        28.8m,       // T8
    };

    public static decimal GetNutritionPerOutput(int tier, int enchant)
    {
        if (tier < 2 || tier >= BaseNutritionPerTier.Length) return 0;
        var multiplier = enchant switch
        {
            0 => 1m,
            1 => 2m,
            2 => 4m,
            3 => 8m,
            4 => 16m,
            _ => 1m,
        };
        return BaseNutritionPerTier[tier] * multiplier;
    }

    public static long GetStationFeePerOutput(int tier, int enchant, decimal stationFeePer100Nutrition)
    {
        if (stationFeePer100Nutrition <= 0m) return 0;
        var nutrition = GetNutritionPerOutput(tier, enchant);
        if (nutrition <= 0m) return 0;
        var fee = nutrition * stationFeePer100Nutrition / 100m;
        return fee <= 0m ? 0 : (long)Math.Round(fee);
    }

    public static IEnumerable<(int Tier, int Enchant)> AllNodes()
    {
        for (var t = MinTier; t <= MaxTier; t++)
            for (var e = MinEnchant; e <= MaxEnchant; e++)
                yield return (t, e);
    }
}