using System.ComponentModel.DataAnnotations;

namespace WatchTrack.Models;

public class Series
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int? ReleaseYear { get; set; }

    [MaxLength(100)]
    public string? Genre { get; set; }

    [MaxLength(500)]
    public string? PosterUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Season> Seasons { get; set; } = new List<Season>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
