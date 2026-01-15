using Microsoft.AspNetCore.Mvc;
using WatchTrack.DTOs;
using WatchTrack.Services;

namespace WatchTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService) => _movieService = movieService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll() => Ok(await _movieService.GetAllMoviesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        return movie == null ? NotFound() : Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> Create(CreateMovieDto createDto)
    {
        var movie = await _movieService.CreateMovieAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieDto>> Update(int id, UpdateMovieDto updateDto)
    {
        var movie = await _movieService.UpdateMovieAsync(id, updateDto);
        return movie == null ? NotFound() : Ok(movie);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _movieService.DeleteMovieAsync(id);
        return !result ? NotFound() : NoContent();
    }
}
