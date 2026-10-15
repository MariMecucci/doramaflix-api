namespace DoramaFlix.Api.Models;

public class Drama
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public int Episodes { get; set; }

    public double Rating { get; set; }

    public bool IsCompleted { get; set; }
}