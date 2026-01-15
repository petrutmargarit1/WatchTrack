using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTrack.Models;

public class WatchHistory
{
    [Key]
    public int Id { get; set; }

    public DateTime WatchedAt { get; set; } = DateTime.UtcNow;

    public bool Completed { get; set; } = true;

    // Foreign keys
    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    // Either MovieId or EpisodeId should be set
    public int? MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }

    public int? EpisodeId { get; set; }

    [ForeignKey(nameof(EpisodeId))]
    public Episode? Episode { get; set; }
}
