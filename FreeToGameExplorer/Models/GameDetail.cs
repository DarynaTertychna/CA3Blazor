namespace FreeToGameExplorer.Models;

public class GameDetail
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Thumbnail { get; set; } = string.Empty;
	public string Status { get; set; } = string.Empty;
	public string ShortDescription { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string GameUrl { get; set; } = string.Empty;
	public string Genre { get; set; } = string.Empty;
	public string Platform { get; set; } = string.Empty;
	public string Publisher { get; set; } = string.Empty;
	public string Developer { get; set; } = string.Empty;
	public string ReleaseDate { get; set; } = string.Empty;

	public MinimumSystemRequirements? MinimumSystemRequirements { get; set; }
	public List<Screenshot> Screenshots { get; set; } = new();
}

public class MinimumSystemRequirements
{
	public string Os { get; set; } = string.Empty;
	public string Processor { get; set; } = string.Empty;
	public string Memory { get; set; } = string.Empty;
	public string Graphics { get; set; } = string.Empty;
	public string Storage { get; set; } = string.Empty;
}

public class Screenshot
{
	public int Id { get; set; }
	public string Image { get; set; } = string.Empty;
}
