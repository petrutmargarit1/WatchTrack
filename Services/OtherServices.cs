using Microsoft.EntityFrameworkCore;
using WatchTrack.Data;
using WatchTrack.DTOs;
using WatchTrack.Models;

namespace WatchTrack.Services;

// Episode Service
public interface IEpisodeService
{
    Task<IEnumerable<EpisodeDto>> GetAllEpisodesAsync();
    Task<EpisodeDto?> GetEpisodeByIdAsync(int id);
    Task<IEnumerable<EpisodeDto>> GetEpisodesBySeasonIdAsync(int seasonId);
    Task<EpisodeDto> CreateEpisodeAsync(CreateEpisodeDto createDto);
    Task<EpisodeDto?> UpdateEpisodeAsync(int id, UpdateEpisodeDto updateDto);
    Task<bool> DeleteEpisodeAsync(int id);
}

public class EpisodeService : IEpisodeService
{
    private readonly WatchTrackDbContext _context;

    public EpisodeService(WatchTrackDbContext context) => _context = context;

    public async Task<IEnumerable<EpisodeDto>> GetAllEpisodesAsync() =>
        await _context.Episodes.Select(e => new EpisodeDto
        {
            Id = e.Id,
            EpisodeNumber = e.EpisodeNumber,
            Title = e.Title,
            Description = e.Description,
            DurationMinutes = e.DurationMinutes,
            AirDate = e.AirDate,
            SeasonId = e.SeasonId
        }).ToListAsync();

    public async Task<EpisodeDto?> GetEpisodeByIdAsync(int id)
    {
        var episode = await _context.Episodes.FindAsync(id);
        return episode == null ? null : new EpisodeDto
        {
            Id = episode.Id,
            EpisodeNumber = episode.EpisodeNumber,
            Title = episode.Title,
            Description = episode.Description,
            DurationMinutes = episode.DurationMinutes,
            AirDate = episode.AirDate,
            SeasonId = episode.SeasonId
        };
    }

    public async Task<IEnumerable<EpisodeDto>> GetEpisodesBySeasonIdAsync(int seasonId) =>
        await _context.Episodes.Where(e => e.SeasonId == seasonId).Select(e => new EpisodeDto
        {
            Id = e.Id,
            EpisodeNumber = e.EpisodeNumber,
            Title = e.Title,
            Description = e.Description,
            DurationMinutes = e.DurationMinutes,
            AirDate = e.AirDate,
            SeasonId = e.SeasonId
        }).ToListAsync();

    public async Task<EpisodeDto> CreateEpisodeAsync(CreateEpisodeDto createDto)
    {
        var episode = new Episode
        {
            EpisodeNumber = createDto.EpisodeNumber,
            Title = createDto.Title,
            Description = createDto.Description,
            DurationMinutes = createDto.DurationMinutes,
            AirDate = createDto.AirDate,
            SeasonId = createDto.SeasonId
        };
        _context.Episodes.Add(episode);
        await _context.SaveChangesAsync();
        return new EpisodeDto
        {
            Id = episode.Id,
            EpisodeNumber = episode.EpisodeNumber,
            Title = episode.Title,
            Description = episode.Description,
            DurationMinutes = episode.DurationMinutes,
            AirDate = episode.AirDate,
            SeasonId = episode.SeasonId
        };
    }

    public async Task<EpisodeDto?> UpdateEpisodeAsync(int id, UpdateEpisodeDto updateDto)
    {
        var episode = await _context.Episodes.FindAsync(id);
        if (episode == null) return null;

        if (updateDto.EpisodeNumber.HasValue) episode.EpisodeNumber = updateDto.EpisodeNumber.Value;
        if (updateDto.Title != null) episode.Title = updateDto.Title;
        if (updateDto.Description != null) episode.Description = updateDto.Description;
        if (updateDto.DurationMinutes.HasValue) episode.DurationMinutes = updateDto.DurationMinutes;
        if (updateDto.AirDate.HasValue) episode.AirDate = updateDto.AirDate;

        await _context.SaveChangesAsync();
        return new EpisodeDto
        {
            Id = episode.Id,
            EpisodeNumber = episode.EpisodeNumber,
            Title = episode.Title,
            Description = episode.Description,
            DurationMinutes = episode.DurationMinutes,
            AirDate = episode.AirDate,
            SeasonId = episode.SeasonId
        };
    }

    public async Task<bool> DeleteEpisodeAsync(int id)
    {
        var episode = await _context.Episodes.FindAsync(id);
        if (episode == null) return false;
        _context.Episodes.Remove(episode);
        await _context.SaveChangesAsync();
        return true;
    }
}

// Review Service
public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
    Task<ReviewDto?> GetReviewByIdAsync(int id);
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto createDto);
    Task<ReviewDto?> UpdateReviewAsync(int id, UpdateReviewDto updateDto);
    Task<bool> DeleteReviewAsync(int id);
}

public class ReviewService : IReviewService
{
    private readonly WatchTrackDbContext _context;

    public ReviewService(WatchTrackDbContext context) => _context = context;

    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync() =>
        await _context.Reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt,
            UserId = r.UserId,
            MovieId = r.MovieId,
            SeriesId = r.SeriesId
        }).ToListAsync();

    public async Task<ReviewDto?> GetReviewByIdAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        return review == null ? null : new ReviewDto
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            UserId = review.UserId,
            MovieId = review.MovieId,
            SeriesId = review.SeriesId
        };
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createDto)
    {
        var review = new Review
        {
            Rating = createDto.Rating,
            Comment = createDto.Comment,
            UserId = createDto.UserId,
            MovieId = createDto.MovieId,
            SeriesId = createDto.SeriesId
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return new ReviewDto
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            UserId = review.UserId,
            MovieId = review.MovieId,
            SeriesId = review.SeriesId
        };
    }

    public async Task<ReviewDto?> UpdateReviewAsync(int id, UpdateReviewDto updateDto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return null;

        if (updateDto.Rating.HasValue) review.Rating = updateDto.Rating.Value;
        if (updateDto.Comment != null) review.Comment = updateDto.Comment;

        await _context.SaveChangesAsync();
        return new ReviewDto
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            UserId = review.UserId,
            MovieId = review.MovieId,
            SeriesId = review.SeriesId
        };
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return false;
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }
}

// WatchHistory Service
public interface IWatchHistoryService
{
    Task<IEnumerable<WatchHistoryDto>> GetAllWatchHistoriesAsync();
    Task<WatchHistoryDto?> GetWatchHistoryByIdAsync(int id);
    Task<IEnumerable<WatchHistoryDto>> GetWatchHistoriesByUserIdAsync(int userId);
    Task<WatchHistoryDto> CreateWatchHistoryAsync(CreateWatchHistoryDto createDto);
    Task<WatchHistoryDto?> UpdateWatchHistoryAsync(int id, UpdateWatchHistoryDto updateDto);
    Task<bool> DeleteWatchHistoryAsync(int id);
}

public class WatchHistoryService : IWatchHistoryService
{
    private readonly WatchTrackDbContext _context;

    public WatchHistoryService(WatchTrackDbContext context) => _context = context;

    public async Task<IEnumerable<WatchHistoryDto>> GetAllWatchHistoriesAsync() =>
        await _context.WatchHistories.Select(wh => new WatchHistoryDto
        {
            Id = wh.Id,
            WatchedAt = wh.WatchedAt,
            Completed = wh.Completed,
            UserId = wh.UserId,
            MovieId = wh.MovieId,
            EpisodeId = wh.EpisodeId
        }).ToListAsync();

    public async Task<WatchHistoryDto?> GetWatchHistoryByIdAsync(int id)
    {
        var wh = await _context.WatchHistories.FindAsync(id);
        return wh == null ? null : new WatchHistoryDto
        {
            Id = wh.Id,
            WatchedAt = wh.WatchedAt,
            Completed = wh.Completed,
            UserId = wh.UserId,
            MovieId = wh.MovieId,
            EpisodeId = wh.EpisodeId
        };
    }

    public async Task<IEnumerable<WatchHistoryDto>> GetWatchHistoriesByUserIdAsync(int userId) =>
        await _context.WatchHistories.Where(wh => wh.UserId == userId).Select(wh => new WatchHistoryDto
        {
            Id = wh.Id,
            WatchedAt = wh.WatchedAt,
            Completed = wh.Completed,
            UserId = wh.UserId,
            MovieId = wh.MovieId,
            EpisodeId = wh.EpisodeId
        }).ToListAsync();

    public async Task<WatchHistoryDto> CreateWatchHistoryAsync(CreateWatchHistoryDto createDto)
    {
        var wh = new WatchHistory
        {
            Completed = createDto.Completed,
            UserId = createDto.UserId,
            MovieId = createDto.MovieId,
            EpisodeId = createDto.EpisodeId
        };
        _context.WatchHistories.Add(wh);
        await _context.SaveChangesAsync();
        return new WatchHistoryDto
        {
            Id = wh.Id,
            WatchedAt = wh.WatchedAt,
            Completed = wh.Completed,
            UserId = wh.UserId,
            MovieId = wh.MovieId,
            EpisodeId = wh.EpisodeId
        };
    }

    public async Task<WatchHistoryDto?> UpdateWatchHistoryAsync(int id, UpdateWatchHistoryDto updateDto)
    {
        var wh = await _context.WatchHistories.FindAsync(id);
        if (wh == null) return null;

        if (updateDto.Completed.HasValue) wh.Completed = updateDto.Completed.Value;

        await _context.SaveChangesAsync();
        return new WatchHistoryDto
        {
            Id = wh.Id,
            WatchedAt = wh.WatchedAt,
            Completed = wh.Completed,
            UserId = wh.UserId,
            MovieId = wh.MovieId,
            EpisodeId = wh.EpisodeId
        };
    }

    public async Task<bool> DeleteWatchHistoryAsync(int id)
    {
        var wh = await _context.WatchHistories.FindAsync(id);
        if (wh == null) return false;
        _context.WatchHistories.Remove(wh);
        await _context.SaveChangesAsync();
        return true;
    }
}

// Watchlist Service
public interface IWatchlistService
{
    Task<IEnumerable<WatchlistDto>> GetAllWatchlistsAsync();
    Task<WatchlistDto?> GetWatchlistByIdAsync(int id);
    Task<IEnumerable<WatchlistDto>> GetWatchlistsByUserIdAsync(int userId);
    Task<WatchlistDto> CreateWatchlistAsync(CreateWatchlistDto createDto);
    Task<bool> DeleteWatchlistAsync(int id);
}

public class WatchlistService : IWatchlistService
{
    private readonly WatchTrackDbContext _context;

    public WatchlistService(WatchTrackDbContext context) => _context = context;

    public async Task<IEnumerable<WatchlistDto>> GetAllWatchlistsAsync() =>
        await _context.Watchlists.Select(w => new WatchlistDto
        {
            Id = w.Id,
            AddedAt = w.AddedAt,
            UserId = w.UserId,
            MovieId = w.MovieId,
            SeriesId = w.SeriesId
        }).ToListAsync();

    public async Task<WatchlistDto?> GetWatchlistByIdAsync(int id)
    {
        var w = await _context.Watchlists.FindAsync(id);
        return w == null ? null : new WatchlistDto
        {
            Id = w.Id,
            AddedAt = w.AddedAt,
            UserId = w.UserId,
            MovieId = w.MovieId,
            SeriesId = w.SeriesId
        };
    }

    public async Task<IEnumerable<WatchlistDto>> GetWatchlistsByUserIdAsync(int userId) =>
        await _context.Watchlists.Where(w => w.UserId == userId).Select(w => new WatchlistDto
        {
            Id = w.Id,
            AddedAt = w.AddedAt,
            UserId = w.UserId,
            MovieId = w.MovieId,
            SeriesId = w.SeriesId
        }).ToListAsync();

    public async Task<WatchlistDto> CreateWatchlistAsync(CreateWatchlistDto createDto)
    {
        var w = new Watchlist
        {
            UserId = createDto.UserId,
            MovieId = createDto.MovieId,
            SeriesId = createDto.SeriesId
        };
        _context.Watchlists.Add(w);
        await _context.SaveChangesAsync();
        return new WatchlistDto
        {
            Id = w.Id,
            AddedAt = w.AddedAt,
            UserId = w.UserId,
            MovieId = w.MovieId,
            SeriesId = w.SeriesId
        };
    }

    public async Task<bool> DeleteWatchlistAsync(int id)
    {
        var w = await _context.Watchlists.FindAsync(id);
        if (w == null) return false;
        _context.Watchlists.Remove(w);
        await _context.SaveChangesAsync();
        return true;
    }
}
