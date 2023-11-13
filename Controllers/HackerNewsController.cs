using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("{numberOfStories}")]
    public async Task<IEnumerable<HackerNewsStoryDto>> Get(int numberOfStories) => await _hackerNewsService.GetBestStoriesAsync(numberOfStories);
}