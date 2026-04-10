using CraftingCalc.Models;

namespace CraftingCalc.Services;

public class CraftingCartService
{
    private readonly List<CartEntry> _entries = [];

    public IReadOnlyList<CartEntry> Entries => _entries;

    public event Action? OnChange;

    public void Add(CartEntry entry)
    {
        _entries.Add(entry);
        OnChange?.Invoke();
    }

    public void Remove(Guid id)
    {
        _entries.RemoveAll(e => e.Id == id);
        OnChange?.Invoke();
    }

    public void Clear()
    {
        _entries.Clear();
        OnChange?.Invoke();
    }

    public IEnumerable<(string ApiId, string Name, int Total, long TotalCost)> GetAggregated() =>
        _entries
            .SelectMany(e => e.Materials)
            .GroupBy(m => m.ApiId)
            .Select(g => (
                ApiId:     g.Key,
                Name:      g.First().Name,
                Total:     g.Sum(m => m.Required),
                TotalCost: g.Sum(m => (long)m.Required * m.Price)
            ))
            .OrderBy(x => x.Name).ThenBy(x => x.ApiId);
}
