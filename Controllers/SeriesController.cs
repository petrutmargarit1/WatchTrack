using Microsoft.AspNetCore.Mvc;
using WatchTrack.DTOs;
using WatchTrack.Services;

namespace WatchTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly ISeriesService _seriesService;

    public SeriesController(ISeriesService seriesService) => _seriesService = seriesService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeriesDto>>> GetAll() => Ok(await _seriesService.GetAllSeriesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<SeriesDto>> GetById(int id)
    {
        var series = await _seriesService.GetSeriesByIdAsync(id);
        return series == null ? NotFound() : Ok(series);
    }

    [HttpPost]
    public async Task<ActionResult<SeriesDto>> Create(CreateSeriesDto createDto)
    {
        var series = await _seriesService.CreateSeriesAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = series.Id }, series);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SeriesDto>> Update(int id, UpdateSeriesDto updateDto)
    {
        var series = await _seriesService.UpdateSeriesAsync(id, updateDto);
        return series == null ? NotFound() : Ok(series);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _seriesService.DeleteSeriesAsync(id);
        return !result ? NotFound() : NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class SeasonsController : ControllerBase
{
    private readonly ISeasonService _seasonService;

    public SeasonsController(ISeasonService seasonService) => _seasonService = seasonService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeasonDto>>> GetAll() => Ok(await _seasonService.GetAllSeasonsAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<SeasonDto>> GetById(int id)
    {
        var season = await _seasonService.GetSeasonByIdAsync(id);
        return season == null ? NotFound() : Ok(season);
    }

    [HttpGet("series/{seriesId}")]
    public async Task<ActionResult<IEnumerable<SeasonDto>>> GetBySeriesId(int seriesId) => 
        Ok(await _seasonService.GetSeasonsBySeriesIdAsync(seriesId));

    [HttpPost]
    public async Task<ActionResult<SeasonDto>> Create(CreateSeasonDto createDto)
    {
        var season = await _seasonService.CreateSeasonAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = season.Id }, season);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SeasonDto>> Update(int id, UpdateSeasonDto updateDto)
    {
        var season = await _seasonService.UpdateSeasonAsync(id, updateDto);
        return season == null ? NotFound() : Ok(season);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _seasonService.DeleteSeasonAsync(id);
        return !result ? NotFound() : NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class EpisodesController : ControllerBase
{
    private readonly IEpisodeService _episodeService;

    public EpisodesController(IEpisodeService episodeService) => _episodeService = episodeService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetAll() => Ok(await _episodeService.GetAllEpisodesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<EpisodeDto>> GetById(int id)
    {
        var episode = await _episodeService.GetEpisodeByIdAsync(id);
        return episode == null ? NotFound() : Ok(episode);
    }

    [HttpGet("season/{seasonId}")]
    public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetBySeasonId(int seasonId) => 
        Ok(await _episodeService.GetEpisodesBySeasonIdAsync(seasonId));

    [HttpPost]
    public async Task<ActionResult<EpisodeDto>> Create(CreateEpisodeDto createDto)
    {
        var episode = await _episodeService.CreateEpisodeAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = episode.Id }, episode);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EpisodeDto>> Update(int id, UpdateEpisodeDto updateDto)
    {
        var episode = await _episodeService.UpdateEpisodeAsync(id, updateDto);
        return episode == null ? NotFound() : Ok(episode);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _episodeService.DeleteEpisodeAsync(id);
        return !result ? NotFound() : NoContent();
    }
}
