using System.Net.Http.Json;
using FreeToGameExplorer.Models;

namespace FreeToGameExplorer.Services;

public class GameApiClient : IGameApiClient
{
    private readonly HttpClient _http;

    public GameApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<GameSummary>> GetGamesAsync(
        string? titleFilter = null,
        string? genre = null,
        string? platform = null,
        string? sortBy = null)
    {
        var url = "games";

        var queryParams = new List<string>();

        if (!string.IsNullOrWhiteSpace(genre))
            queryParams.Add($"category={Uri.EscapeDataString(genre)}");

        if (!string.IsNullOrWhiteSpace(platform))
            queryParams.Add($"platform={Uri.EscapeDataString(platform)}");

        if (!string.IsNullOrWhiteSpace(sortBy))
            queryParams.Add($"sort-by={Uri.EscapeDataString(sortBy)}");

        if (queryParams.Count > 0)
            url += "?" + string.Join("&", queryParams);

        var games = await _http.GetFromJsonAsync<List<GameSummary>>(url)
                    ?? new List<GameSummary>();

        if (!string.IsNullOrWhiteSpace(titleFilter))
        {
            var lower = titleFilter.Trim().ToLowerInvariant();
            games = games
                .Where(g => g.Title.ToLowerInvariant().Contains(lower))
                .ToList();
        }

        return games;
    }

    public async Task<GameDetail?> GetGameAsync(int id)
    {
        var url = $"game?id={id}";
        return await _http.GetFromJsonAsync<GameDetail>(url);
    }
}
