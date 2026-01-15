namespace WatchTrack.DTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public int? DurationMinutes { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public int? DurationMinutes { get; set; }
    public string? PosterUrl { get; set; }
}

public class UpdateMovieDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public int? DurationMinutes { get; set; }
    public string? PosterUrl { get; set; }
}
