using Microsoft.AspNetCore.Mvc;
using WatchTrack.DTOs;
using WatchTrack.Services;

namespace WatchTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService) => _reviewService = reviewService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll() => Ok(await _reviewService.GetAllReviewsAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById(int id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);
        return review == null ? NotFound() : Ok(review);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create(CreateReviewDto createDto)
    {
        var review = await _reviewService.CreateReviewAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReviewDto>> Update(int id, UpdateReviewDto updateDto)
    {
        var review = await _reviewService.UpdateReviewAsync(id, updateDto);
        return review == null ? NotFound() : Ok(review);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _reviewService.DeleteReviewAsync(id);
        return !result ? NotFound() : NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class WatchHistoryController : ControllerBase
{
    private readonly IWatchHistoryService _watchHistoryService;

    public WatchHistoryController(IWatchHistoryService watchHistoryService) => _watchHistoryService = watchHistoryService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WatchHistoryDto>>> GetAll() => 
        Ok(await _watchHistoryService.GetAllWatchHistoriesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<WatchHistoryDto>> GetById(int id)
    {
        var wh = await _watchHistoryService.GetWatchHistoryByIdAsync(id);
        return wh == null ? NotFound() : Ok(wh);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<WatchHistoryDto>>> GetByUserId(int userId) => 
        Ok(await _watchHistoryService.GetWatchHistoriesByUserIdAsync(userId));

    [HttpPost]
    public async Task<ActionResult<WatchHistoryDto>> Create(CreateWatchHistoryDto createDto)
    {
        var wh = await _watchHistoryService.CreateWatchHistoryAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = wh.Id }, wh);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WatchHistoryDto>> Update(int id, UpdateWatchHistoryDto updateDto)
    {
        var wh = await _watchHistoryService.UpdateWatchHistoryAsync(id, updateDto);
        return wh == null ? NotFound() : Ok(wh);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _watchHistoryService.DeleteWatchHistoryAsync(id);
        return !result ? NotFound() : NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class WatchlistController : ControllerBase
{
    private readonly IWatchlistService _watchlistService;

    public WatchlistController(IWatchlistService watchlistService) => _watchlistService = watchlistService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WatchlistDto>>> GetAll() => 
        Ok(await _watchlistService.GetAllWatchlistsAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<WatchlistDto>> GetById(int id)
    {
        var w = await _watchlistService.GetWatchlistByIdAsync(id);
        return w == null ? NotFound() : Ok(w);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<WatchlistDto>>> GetByUserId(int userId) => 
        Ok(await _watchlistService.GetWatchlistsByUserIdAsync(userId));

    [HttpPost]
    public async Task<ActionResult<WatchlistDto>> Create(CreateWatchlistDto createDto)
    {
        var w = await _watchlistService.CreateWatchlistAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = w.Id }, w);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _watchlistService.DeleteWatchlistAsync(id);
        return !result ? NotFound() : NoContent();
    }
}
