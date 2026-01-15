namespace WatchTrack.DTOs;

public class SeriesDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateSeriesDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public string? PosterUrl { get; set; }
}

public class UpdateSeriesDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public string? PosterUrl { get; set; }
}
