using CraftingCalc.Models;

namespace CraftingCalc.Services;

public static class RawResourceDatabase
{
    public const int MinTier = 4;
    public const int MaxTier = 8;
    public const int MinEnchant = 0;
    public const int MaxEnchant = 4;

    // Transmute tier-up requires a T4+ source
    public const int TransmuteMinTier = 4;

    public static string GetTypeCode(RawResourceType type) => type switch
    {
        RawResourceType.Wood  => "WOOD",
        RawResourceType.Ore   => "ORE",
        RawResourceType.Hide  => "HIDE",
        RawResourceType.Fiber => "FIBER",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    public static string GetDisplayName(RawResourceType type) => type switch
    {
        RawResourceType.Wood  => "Wood",
        RawResourceType.Ore   => "Ore",
        RawResourceType.Hide  => "Hide",
        RawResourceType.Fiber => "Fiber",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    // Albion raw resource preferred buy cities
    public static string GetPreferredCity(RawResourceType type) => type switch
    {
        RawResourceType.Wood  => "Fort Sterling",
        RawResourceType.Ore   => "Thetford",
        RawResourceType.Hide  => "Martlock",
        RawResourceType.Fiber => "Lymhurst",
        _ => "Caerleon"
    };

    public static string GetApiId(RawResourceType type, int tier, int enchant)
    {
        var code = GetTypeCode(type);
        var suffix = enchant > 0 ? $"_LEVEL{enchant}@{enchant}" : "";
        return $"T{tier}_{code}{suffix}";
    }

    public static IEnumerable<(int Tier, int Enchant)> AllNodes()
    {
        for (var t = MinTier; t <= MaxTier; t++)
            for (var e = MinEnchant; e <= MaxEnchant; e++)
                yield return (t, e);
    }
}