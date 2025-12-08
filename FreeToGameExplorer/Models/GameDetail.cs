using System.Text.Json.Serialization;

namespace FreeToGameExplorer.Models;

public class GameDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; } = string.Empty;

    [JsonPropertyName("genre")]
    public string Genre { get; set; } = string.Empty;

    [JsonPropertyName("platform")]
    public string Platform { get; set; } = string.Empty;

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = string.Empty;

    [JsonPropertyName("developer")]
    public string Developer { get; set; } = string.Empty;

    //again release date, now is working
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("game_url")]
    public string GameUrl { get; set; } = string.Empty;

    [JsonPropertyName("minimum_system_requirements")]
    public MinimumSystemRequirements? MinimumSystemRequirements { get; set; }

    [JsonPropertyName("screenshots")]
    public List<Screenshot> Screenshots { get; set; } = new();
}

public class MinimumSystemRequirements
{
    [JsonPropertyName("os")]
    public string? Os { get; set; }

    [JsonPropertyName("processor")]
    public string? Processor { get; set; }

    [JsonPropertyName("memory")]
    public string? Memory { get; set; }

    [JsonPropertyName("graphics")]
    public string? Graphics { get; set; }

    [JsonPropertyName("storage")]
    public string? Storage { get; set; }
}

public class Screenshot
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}
