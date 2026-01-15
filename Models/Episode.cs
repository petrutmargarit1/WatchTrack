using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTrack.Models;

public class Episode
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EpisodeNumber { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int? DurationMinutes { get; set; }

    public DateTime? AirDate { get; set; }

    // Foreign key
    [Required]
    public int SeasonId { get; set; }

    [ForeignKey(nameof(SeasonId))]
    public Season Season { get; set; } = null!;

    // Navigation properties
    public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
}
