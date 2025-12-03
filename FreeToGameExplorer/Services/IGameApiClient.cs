using FreeToGameExplorer.Models;

namespace FreeToGameExplorer.Services;

public interface IGameApiClient
{
	Task<IReadOnlyList<GameSummary>> GetGamesAsync(
		string? titleFilter = null,
		string? genre = null,
		string? platform = null,
		string? sortBy = null);

	Task<GameDetail?> GetGameAsync(int id);
}
