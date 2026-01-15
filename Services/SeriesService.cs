using Microsoft.EntityFrameworkCore;
using WatchTrack.Data;
using WatchTrack.DTOs;
using WatchTrack.Models;

namespace WatchTrack.Services;

public interface ISeriesService
{
    Task<IEnumerable<SeriesDto>> GetAllSeriesAsync();
    Task<SeriesDto?> GetSeriesByIdAsync(int id);
    Task<SeriesDto> CreateSeriesAsync(CreateSeriesDto createDto);
    Task<SeriesDto?> UpdateSeriesAsync(int id, UpdateSeriesDto updateDto);
    Task<bool> DeleteSeriesAsync(int id);
}

public class SeriesService : ISeriesService
{
    private readonly WatchTrackDbContext _context;

    public SeriesService(WatchTrackDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SeriesDto>> GetAllSeriesAsync()
    {
        return await _context.Series
            .Select(s => new SeriesDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                ReleaseYear = s.ReleaseYear,
                Genre = s.Genre,
                PosterUrl = s.PosterUrl,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<SeriesDto?> GetSeriesByIdAsync(int id)
    {
        var series = await _context.Series.FindAsync(id);
        if (series == null) return null;

        return new SeriesDto
        {
            Id = series.Id,
            Title = series.Title,
            Description = series.Description,
            ReleaseYear = series.ReleaseYear,
            Genre = series.Genre,
            PosterUrl = series.PosterUrl,
            CreatedAt = series.CreatedAt
        };
    }

    public async Task<SeriesDto> CreateSeriesAsync(CreateSeriesDto createDto)
    {
        var series = new Series
        {
            Title = createDto.Title,
            Description = createDto.Description,
            ReleaseYear = createDto.ReleaseYear,
            Genre = createDto.Genre,
            PosterUrl = createDto.PosterUrl
        };

        _context.Series.Add(series);
        await _context.SaveChangesAsync();

        return new SeriesDto
        {
            Id = series.Id,
            Title = series.Title,
            Description = series.Description,
            ReleaseYear = series.ReleaseYear,
            Genre = series.Genre,
            PosterUrl = series.PosterUrl,
            CreatedAt = series.CreatedAt
        };
    }

    public async Task<SeriesDto?> UpdateSeriesAsync(int id, UpdateSeriesDto updateDto)
    {
        var series = await _context.Series.FindAsync(id);
        if (series == null) return null;

        if (updateDto.Title != null) series.Title = updateDto.Title;
        if (updateDto.Description != null) series.Description = updateDto.Description;
        if (updateDto.ReleaseYear.HasValue) series.ReleaseYear = updateDto.ReleaseYear;
        if (updateDto.Genre != null) series.Genre = updateDto.Genre;
        if (updateDto.PosterUrl != null) series.PosterUrl = updateDto.PosterUrl;

        await _context.SaveChangesAsync();

        return new SeriesDto
        {
            Id = series.Id,
            Title = series.Title,
            Description = series.Description,
            ReleaseYear = series.ReleaseYear,
            Genre = series.Genre,
            PosterUrl = series.PosterUrl,
            CreatedAt = series.CreatedAt
        };
    }

    public async Task<bool> DeleteSeriesAsync(int id)
    {
        var series = await _context.Series.FindAsync(id);
        if (series == null) return false;

        _context.Series.Remove(series);
        await _context.SaveChangesAsync();
        return true;
    }
}

public interface ISeasonService
{
    Task<IEnumerable<SeasonDto>> GetAllSeasonsAsync();
    Task<SeasonDto?> GetSeasonByIdAsync(int id);
    Task<IEnumerable<SeasonDto>> GetSeasonsBySeriesIdAsync(int seriesId);
    Task<SeasonDto> CreateSeasonAsync(CreateSeasonDto createDto);
    Task<SeasonDto?> UpdateSeasonAsync(int id, UpdateSeasonDto updateDto);
    Task<bool> DeleteSeasonAsync(int id);
}

public class SeasonService : ISeasonService
{
    private readonly WatchTrackDbContext _context;

    public SeasonService(WatchTrackDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SeasonDto>> GetAllSeasonsAsync()
    {
        return await _context.Seasons
            .Select(s => new SeasonDto
            {
                Id = s.Id,
                SeasonNumber = s.SeasonNumber,
                Title = s.Title,
                ReleaseYear = s.ReleaseYear,
                SeriesId = s.SeriesId
            })
            .ToListAsync();
    }

    public async Task<SeasonDto?> GetSeasonByIdAsync(int id)
    {
        var season = await _context.Seasons.FindAsync(id);
        if (season == null) return null;

        return new SeasonDto
        {
            Id = season.Id,
            SeasonNumber = season.SeasonNumber,
            Title = season.Title,
            ReleaseYear = season.ReleaseYear,
            SeriesId = season.SeriesId
        };
    }

    public async Task<IEnumerable<SeasonDto>> GetSeasonsBySeriesIdAsync(int seriesId)
    {
        return await _context.Seasons
            .Where(s => s.SeriesId == seriesId)
            .Select(s => new SeasonDto
            {
                Id = s.Id,
                SeasonNumber = s.SeasonNumber,
                Title = s.Title,
                ReleaseYear = s.ReleaseYear,
                SeriesId = s.SeriesId
            })
            .ToListAsync();
    }

    public async Task<SeasonDto> CreateSeasonAsync(CreateSeasonDto createDto)
    {
        var season = new Season
        {
            SeasonNumber = createDto.SeasonNumber,
            Title = createDto.Title,
            ReleaseYear = createDto.ReleaseYear,
            SeriesId = createDto.SeriesId
        };

        _context.Seasons.Add(season);
        await _context.SaveChangesAsync();

        return new SeasonDto
        {
            Id = season.Id,
            SeasonNumber = season.SeasonNumber,
            Title = season.Title,
            ReleaseYear = season.ReleaseYear,
            SeriesId = season.SeriesId
        };
    }

    public async Task<SeasonDto?> UpdateSeasonAsync(int id, UpdateSeasonDto updateDto)
    {
        var season = await _context.Seasons.FindAsync(id);
        if (season == null) return null;

        if (updateDto.SeasonNumber.HasValue) season.SeasonNumber = updateDto.SeasonNumber.Value;
        if (updateDto.Title != null) season.Title = updateDto.Title;
        if (updateDto.ReleaseYear.HasValue) season.ReleaseYear = updateDto.ReleaseYear;

        await _context.SaveChangesAsync();

        return new SeasonDto
        {
            Id = season.Id,
            SeasonNumber = season.SeasonNumber,
            Title = season.Title,
            ReleaseYear = season.ReleaseYear,
            SeriesId = season.SeriesId
        };
    }

    public async Task<bool> DeleteSeasonAsync(int id)
    {
        var season = await _context.Seasons.FindAsync(id);
        if (season == null) return false;

        _context.Seasons.Remove(season);
        await _context.SaveChangesAsync();
        return true;
    }
}
