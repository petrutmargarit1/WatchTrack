namespace WatchTrack.DTOs;

// Season DTOs
public class SeasonDto
{
    public int Id { get; set; }
    public int SeasonNumber { get; set; }
    public string? Title { get; set; }
    public int? ReleaseYear { get; set; }
    public int SeriesId { get; set; }
}

public class CreateSeasonDto
{
    public int SeasonNumber { get; set; }
    public string? Title { get; set; }
    public int? ReleaseYear { get; set; }
    public int SeriesId { get; set; }
}

public class UpdateSeasonDto
{
    public int? SeasonNumber { get; set; }
    public string? Title { get; set; }
    public int? ReleaseYear { get; set; }
}

// Episode DTOs
public class EpisodeDto
{
    public int Id { get; set; }
    public int EpisodeNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? DurationMinutes { get; set; }
    public DateTime? AirDate { get; set; }
    public int SeasonId { get; set; }
}

public class CreateEpisodeDto
{
    public int EpisodeNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? DurationMinutes { get; set; }
    public DateTime? AirDate { get; set; }
    public int SeasonId { get; set; }
}

public class UpdateEpisodeDto
{
    public int? EpisodeNumber { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? DurationMinutes { get; set; }
    public DateTime? AirDate { get; set; }
}

// Review DTOs
public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int? MovieId { get; set; }
    public int? SeriesId { get; set; }
}

public class CreateReviewDto
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public int UserId { get; set; }
    public int? MovieId { get; set; }
    public int? SeriesId { get; set; }
}

public class UpdateReviewDto
{
    public int? Rating { get; set; }
    public string? Comment { get; set; }
}

// WatchHistory DTOs
public class WatchHistoryDto
{
    public int Id { get; set; }
    public DateTime WatchedAt { get; set; }
    public bool Completed { get; set; }
    public int UserId { get; set; }
    public int? MovieId { get; set; }
    public int? EpisodeId { get; set; }
}

public class CreateWatchHistoryDto
{
    public bool Completed { get; set; } = true;
    public int UserId { get; set; }
    public int? MovieId { get; set; }
    public int? EpisodeId { get; set; }
}

public class UpdateWatchHistoryDto
{
    public bool? Completed { get; set; }
}

// Watchlist DTOs
public class WatchlistDto
{
    public int Id { get; set; }
    public DateTime AddedAt { get; set; }
    public int UserId { get; set; }
    public int? MovieId { get; set; }
    public int? SeriesId { get; set; }
}

public class CreateWatchlistDto
{
    public int UserId { get; set; }
    public int? MovieId { get; set; }
    public int? SeriesId { get; set; }
}
