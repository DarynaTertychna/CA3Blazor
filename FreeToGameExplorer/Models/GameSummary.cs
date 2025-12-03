namespace FreeToGameExplorer.Models;

public class GameSummary
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Thumbnail { get; set; } = string.Empty;
	public string ShortDescription { get; set; } = string.Empty;
	public string GameUrl { get; set; } = string.Empty;
	public string Genre { get; set; } = string.Empty;
	public string Platform { get; set; } = string.Empty;
	public string Publisher { get; set; } = string.Empty;
	public string Developer { get; set; } = string.Empty;
	public string ReleaseDate { get; set; } = string.Empty;
}
