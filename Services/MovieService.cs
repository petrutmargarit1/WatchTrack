using Microsoft.EntityFrameworkCore;
using WatchTrack.Data;
using WatchTrack.DTOs;
using WatchTrack.Models;

namespace WatchTrack.Services;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto?> GetMovieByIdAsync(int id);
    Task<MovieDto> CreateMovieAsync(CreateMovieDto createDto);
    Task<MovieDto?> UpdateMovieAsync(int id, UpdateMovieDto updateDto);
    Task<bool> DeleteMovieAsync(int id);
}

public class MovieService : IMovieService
{
    private readonly WatchTrackDbContext _context;

    public MovieService(WatchTrackDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        return await _context.Movies
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Genre = m.Genre,
                DurationMinutes = m.DurationMinutes,
                PosterUrl = m.PosterUrl,
                CreatedAt = m.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<MovieDto?> GetMovieByIdAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return null;

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseYear = movie.ReleaseYear,
            Genre = movie.Genre,
            DurationMinutes = movie.DurationMinutes,
            PosterUrl = movie.PosterUrl,
            CreatedAt = movie.CreatedAt
        };
    }

    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createDto)
    {
        var movie = new Movie
        {
            Title = createDto.Title,
            Description = createDto.Description,
            ReleaseYear = createDto.ReleaseYear,
            Genre = createDto.Genre,
            DurationMinutes = createDto.DurationMinutes,
            PosterUrl = createDto.PosterUrl
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseYear = movie.ReleaseYear,
            Genre = movie.Genre,
            DurationMinutes = movie.DurationMinutes,
            PosterUrl = movie.PosterUrl,
            CreatedAt = movie.CreatedAt
        };
    }

    public async Task<MovieDto?> UpdateMovieAsync(int id, UpdateMovieDto updateDto)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return null;

        if (updateDto.Title != null) movie.Title = updateDto.Title;
        if (updateDto.Description != null) movie.Description = updateDto.Description;
        if (updateDto.ReleaseYear.HasValue) movie.ReleaseYear = updateDto.ReleaseYear;
        if (updateDto.Genre != null) movie.Genre = updateDto.Genre;
        if (updateDto.DurationMinutes.HasValue) movie.DurationMinutes = updateDto.DurationMinutes;
        if (updateDto.PosterUrl != null) movie.PosterUrl = updateDto.PosterUrl;

        await _context.SaveChangesAsync();

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseYear = movie.ReleaseYear,
            Genre = movie.Genre,
            DurationMinutes = movie.DurationMinutes,
            PosterUrl = movie.PosterUrl,
            CreatedAt = movie.CreatedAt
        };
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }
}
