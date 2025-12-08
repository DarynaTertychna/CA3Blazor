using System.Net.Http.Json;
using FreeToGameExplorer.Models;

namespace FreeToGameExplorer.Services;

//I wil change names later (if I have time)
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

        //add additional params later (but I have enough to meet my CA requirements now)
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            url += $"?sort-by={Uri.EscapeDataString(sortBy)}";
        }

        //api is working but for handling
        var games = await _http.GetFromJsonAsync<List<GameSummary>>(url)
                    ?? new List<GameSummary>();

        if (!string.IsNullOrWhiteSpace(titleFilter))
        {
            var lower = titleFilter.Trim().ToLowerInvariant();
            games = games
                .Where(g => g.Title.ToLowerInvariant().Contains(lower))
                .ToList();
        }
        //filtering (I need change css later)
        if (!string.IsNullOrWhiteSpace(genre))
        {
            games = games
                .Where(g =>
                    !string.IsNullOrWhiteSpace(g.Genre) &&
                    string.Equals(g.Genre, genre, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(platform))
        {
            games = games
                .Where(g =>
                    !string.IsNullOrWhiteSpace(g.Platform) &&
                    g.Platform.Contains(platform, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return games;   //that's my game list here
    }

    public async Task<GameDetail?> GetGameAsync(int id)
    {
        var url = $"game?id={id}";
        // null in return if there is nothing
        return await _http.GetFromJsonAsync<GameDetail>(url);
    }
}