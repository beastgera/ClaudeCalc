using System.Text.Json;
using CraftingCalc.Models;

namespace CraftingCalc.Services;

public record AllPricesResult(
    Dictionary<string, long>                      MatPrices,
    Dictionary<string, long>                      ItemBestPrices,
    Dictionary<string, Dictionary<string, long>>  ItemCityPrices,
    Dictionary<string, long>                      ArtifactPrices
);

public class AlbionPriceService(HttpClient http)
{
    private const string BaseUrl = "https://europe.albion-online-data.com/api/v2/stats/prices";

    /// <summary>
    /// Single HTTP request for all prices needed to calculate crafting profit.
    /// Fetches mats + items + artifacts in one call, filters by quality client-side.
    /// </summary>
    public async Task<AllPricesResult> FetchAllForCraftAsync(
        HashSet<string> matIds,
        HashSet<string> itemIds,
        HashSet<string> artifactIds,
        Func<string, string> getMatCity,
        string itemCity,
        string artifactCity,
        bool   useBuyOrder,
        int    itemQuality)
    {
        var allIds = matIds.Concat(itemIds).Concat(artifactIds).Distinct().ToList();
        if (allIds.Count == 0)
            return new AllPricesResult([], [], [], []);

        var ids  = string.Join(",", allIds);
        var locs = string.Join(",", GearItemDatabase.Cities.Select(Uri.EscapeDataString));
        var url  = $"{BaseUrl}/{ids}?locations={locs}";

        var entries = await http.GetFromJsonAsync<List<AlbionMarketEntry>>(url) ?? [];

        return new AllPricesResult(
            MatPrices:      BuildBestPricesPerCity(entries, matIds, getMatCity, useBuyOrder, quality: 1),
            ItemBestPrices: BuildBestPrices(entries, itemIds,      itemCity,     useBuyOrder: false, itemQuality),
            ItemCityPrices: BuildCityPrices(entries, itemIds,      itemQuality),
            ArtifactPrices: BuildBestPrices(entries, artifactIds,  artifactCity, useBuyOrder,       quality: 1)
        );
    }

    // Per-item preferred city resolved via a delegate (used for materials)
    private static Dictionary<string, long> BuildBestPricesPerCity(
        List<AlbionMarketEntry> entries,
        IEnumerable<string> ids,
        Func<string, string> getPrefCity,
        bool useBuyOrder,
        int quality)
    {
        var result = new Dictionary<string, long>();
        foreach (var id in ids)
        {
            var relevant  = entries.Where(e => e.ItemId == id && e.Quality == quality).ToList();
            var preferred = relevant.FirstOrDefault(e => e.City == getPrefCity(id));
            var price     = preferred != null
                ? (useBuyOrder ? preferred.BuyPriceMax : preferred.SellPriceMin)
                : relevant
                    .Select(e => useBuyOrder ? e.BuyPriceMax : e.SellPriceMin)
                    .Where(p => p > 0)
                    .DefaultIfEmpty(0)
                    .Min();
            if (price > 0) result[id] = price;
        }
        return result;
    }

    private static Dictionary<string, long> BuildBestPrices(
        List<AlbionMarketEntry> entries,
        IEnumerable<string> ids,
        string prefCity,
        bool useBuyOrder,
        int quality)
    {
        var result = new Dictionary<string, long>();
        foreach (var id in ids)
        {
            var relevant  = entries.Where(e => e.ItemId == id && e.Quality == quality).ToList();
            var preferred = relevant.FirstOrDefault(e => e.City == prefCity);
            var price     = preferred != null
                ? (useBuyOrder ? preferred.BuyPriceMax : preferred.SellPriceMin)
                : relevant
                    .Select(e => useBuyOrder ? e.BuyPriceMax : e.SellPriceMin)
                    .Where(p => p > 0)
                    .DefaultIfEmpty(0)
                    .Min();
            if (price > 0) result[id] = price;
        }
        return result;
    }

    private static Dictionary<string, Dictionary<string, long>> BuildCityPrices(
        List<AlbionMarketEntry> entries,
        IEnumerable<string> ids,
        int quality)
    {
        var idSet  = ids.ToHashSet();
        var result = new Dictionary<string, Dictionary<string, long>>();
        foreach (var e in entries.Where(e => idSet.Contains(e.ItemId) && e.Quality == quality && e.SellPriceMin > 0))
        {
            if (!result.TryGetValue(e.ItemId, out var cityDict))
                result[e.ItemId] = cityDict = new Dictionary<string, long>();
            cityDict[e.City] = e.SellPriceMin;
        }
        return result;
    }
}
