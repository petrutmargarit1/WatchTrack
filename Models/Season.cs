using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTrack.Models;

public class Season
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SeasonNumber { get; set; }

    [MaxLength(200)]
    public string? Title { get; set; }

    public int? ReleaseYear { get; set; }

    // Foreign key
    [Required]
    public int SeriesId { get; set; }

    [ForeignKey(nameof(SeriesId))]
    public Series Series { get; set; } = null!;

    // Navigation properties
    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
}
