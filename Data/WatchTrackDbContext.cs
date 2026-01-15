using Microsoft.EntityFrameworkCore;
using WatchTrack.Models;

namespace WatchTrack.Data;

public class WatchTrackDbContext : DbContext
{
    public WatchTrackDbContext(DbContextOptions<WatchTrackDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configurations
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Series -> Season relationship
        modelBuilder.Entity<Season>()
            .HasOne(s => s.Series)
            .WithMany(sr => sr.Seasons)
            .HasForeignKey(s => s.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        // Season -> Episode relationship
        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Season)
            .WithMany(s => s.Episodes)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);

        // Review relationships
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Reviews)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Series)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        // WatchHistory relationships
        modelBuilder.Entity<WatchHistory>()
            .HasOne(wh => wh.User)
            .WithMany(u => u.WatchHistories)
            .HasForeignKey(wh => wh.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WatchHistory>()
            .HasOne(wh => wh.Movie)
            .WithMany(m => m.WatchHistories)
            .HasForeignKey(wh => wh.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WatchHistory>()
            .HasOne(wh => wh.Episode)
            .WithMany(e => e.WatchHistories)
            .HasForeignKey(wh => wh.EpisodeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Watchlist relationships
        modelBuilder.Entity<Watchlist>()
            .HasOne(w => w.User)
            .WithMany(u => u.Watchlists)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Watchlist>()
            .HasOne(w => w.Movie)
            .WithMany(m => m.Watchlists)
            .HasForeignKey(w => w.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Watchlist>()
            .HasOne(w => w.Series)
            .WithMany(s => s.Watchlists)
            .HasForeignKey(w => w.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
