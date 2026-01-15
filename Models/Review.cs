using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTrack.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Range(1, 10)]
    public int Rating { get; set; }

    [MaxLength(2000)]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign keys
    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    // Either MovieId or SeriesId should be set (nullable to support both)
    public int? MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }

    public int? SeriesId { get; set; }

    [ForeignKey(nameof(SeriesId))]
    public Series? Series { get; set; }
}
