using Microsoft.JSInterop;
using System.Text.Json;

namespace CraftingCalc.Services;

/// <summary>
/// Tracks per-item specializations (0-100) and per-category masteries (0-100).
/// Converts them to Focus Cost Efficiency (FCE) using Albion Online's model:
///   • Item spec — unique bonus (only that item):
///       250 FCE / level  — always, regardless of item class
///   • Item spec — mutual bonus (applied to every sibling in the same category):
///       Base item     → 30    FCE / level
///       Artifact item → 15    FCE / level
///       Crystal item  →  2.15 FCE / level
///   • Category mastery: 30 FCE / level, applies to every item in the category
/// Focus multiplier = 0.5 ^ (total_FCE / 10_000).  Every 10,000 FCE halves the focus cost.
/// State persists to browser localStorage.
/// </summary>
public class SpecTreeService
{
    private const string ItemStorageKey = "craftingcalc.itemspecs.v2";
    private const string CategoryStorageKey = "craftingcalc.categoryspecs.v2";

    public const int SpecUniqueFcePerLevel = 250;
    public const int MasteryFcePerLevel = 30;
    public const int FceHalvingUnit = 10_000;

    public static decimal MutualFceForClass(GearItemDatabase.ItemClass c) => c switch
    {
        GearItemDatabase.ItemClass.Base     => 30m,
        GearItemDatabase.ItemClass.Artifact => 15m,
        GearItemDatabase.ItemClass.Crystal  => 2.15m,
        _ => 30m
    };

    private Dictionary<string, int> itemSpecs = new();
    private Dictionary<string, int> categorySpecs = new();
    private bool loaded;

    public event Action? OnChange;

    public int GetItemSpec(string itemId) =>
        itemSpecs.TryGetValue(itemId, out var v) ? v : 0;

    public int GetCategoryMastery(string category) =>
        categorySpecs.TryGetValue(category, out var v) ? v : 0;

    /// <summary>Sum of mutual FCE from every sibling's spec (each sibling contributes at its own class rate).</summary>
    public decimal GetCategoryMutualFce(string category)
    {
        var siblings = GearItemDatabase.GetSiblings(category);
        decimal sum = 0m;
        foreach (var s in siblings)
        {
            var lvl = GetItemSpec(s.Id);
            if (lvl == 0) continue;
            sum += lvl * MutualFceForClass(GearItemDatabase.GetItemClass(s.Id));
        }
        return sum;
    }

    /// <summary>Total FCE for crafting the given item — own unique + all sibling mutual + category mastery.</summary>
    public decimal GetFce(string itemId)
    {
        var category = GearItemDatabase.GetCategory(itemId);
        var ownLevel = GetItemSpec(itemId);
        var masteryLevel = GetCategoryMastery(category);

        return ownLevel * SpecUniqueFcePerLevel
             + GetCategoryMutualFce(category)
             + masteryLevel * MasteryFcePerLevel;
    }

    /// <summary>Focus cost multiplier — apply to base focus, e.g. baseFocus * GetFocusMultiplier(id).</summary>
    public decimal GetFocusMultiplier(string itemId)
    {
        var fce = GetFce(itemId);
        return (decimal)Math.Pow(0.5, (double)fce / FceHalvingUnit);
    }

    /// <summary>1 - multiplier — how much focus you save, e.g. 0.475 = 47.5% off.</summary>
    public decimal GetFocusReduction(string itemId) =>
        1m - GetFocusMultiplier(itemId);

    public FceBreakdown GetBreakdown(string itemId)
    {
        var category = GearItemDatabase.GetCategory(itemId);
        var siblings = GearItemDatabase.GetSiblings(category);
        var siblingLevelSum = siblings.Sum(s => GetItemSpec(s.Id));
        return new FceBreakdown
        {
            Category = category,
            ItemClass = GearItemDatabase.GetItemClass(itemId),
            OwnSpec = GetItemSpec(itemId),
            CategoryMastery = GetCategoryMastery(category),
            SiblingLevelSum = siblingLevelSum,
            UniqueFce = GetItemSpec(itemId) * SpecUniqueFcePerLevel,
            MutualFce = GetCategoryMutualFce(category),
            MasteryFce = GetCategoryMastery(category) * MasteryFcePerLevel,
        };
    }

    public async Task LoadAsync(IJSRuntime js)
    {
        if (loaded) return;
        loaded = true;
        try
        {
            var itemJson = await js.InvokeAsync<string?>("localStorage.getItem", ItemStorageKey);
            if (!string.IsNullOrWhiteSpace(itemJson))
            {
                var parsed = JsonSerializer.Deserialize<Dictionary<string, int>>(itemJson);
                if (parsed != null) itemSpecs = parsed;
            }
            var catJson = await js.InvokeAsync<string?>("localStorage.getItem", CategoryStorageKey);
            if (!string.IsNullOrWhiteSpace(catJson))
            {
                var parsed = JsonSerializer.Deserialize<Dictionary<string, int>>(catJson);
                if (parsed != null) categorySpecs = parsed;
            }
        }
        catch { }
    }

    public async Task SetItemSpecAsync(IJSRuntime js, string itemId, int level)
    {
        level = Math.Clamp(level, 0, 100);
        if (level == 0) itemSpecs.Remove(itemId);
        else itemSpecs[itemId] = level;
        await SaveAsync(js, ItemStorageKey, itemSpecs);
        OnChange?.Invoke();
    }

    public async Task SetCategoryMasteryAsync(IJSRuntime js, string category, int level)
    {
        level = Math.Clamp(level, 0, 100);
        if (level == 0) categorySpecs.Remove(category);
        else categorySpecs[category] = level;
        await SaveAsync(js, CategoryStorageKey, categorySpecs);
        OnChange?.Invoke();
    }

    public async Task ResetAsync(IJSRuntime js)
    {
        itemSpecs.Clear();
        categorySpecs.Clear();
        try
        {
            await js.InvokeVoidAsync("localStorage.removeItem", ItemStorageKey);
            await js.InvokeVoidAsync("localStorage.removeItem", CategoryStorageKey);
        }
        catch { }
        OnChange?.Invoke();
    }

    private static async Task SaveAsync(IJSRuntime js, string key, Dictionary<string, int> dict)
    {
        try
        {
            var json = JsonSerializer.Serialize(dict);
            await js.InvokeVoidAsync("localStorage.setItem", key, json);
        }
        catch { }
    }

    public int ItemSpecCount => itemSpecs.Count;
    public int CategoryMasteryCount => categorySpecs.Count;
}

public record FceBreakdown
{
    public string Category { get; init; } = "";
    public GearItemDatabase.ItemClass ItemClass { get; init; }
    public int OwnSpec { get; init; }
    public int CategoryMastery { get; init; }
    public int SiblingLevelSum { get; init; }
    public int UniqueFce { get; init; }
    public decimal MutualFce { get; init; }
    public int MasteryFce { get; init; }
    public decimal TotalFce => UniqueFce + MutualFce + MasteryFce;
}
