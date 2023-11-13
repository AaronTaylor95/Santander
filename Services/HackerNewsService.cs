using Microsoft.Extensions.Caching.Memory;
using SantanderHackerNewsAPI.Extensions;
using SantanderHackerNewsAPI.Models;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public HackerNewsService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient();
        _cache = cache;

    }
    public async Task<IEnumerable<HackerNewsStoryDto>> GetBestStoriesAsync(int numberOfStories)
    {
        var cacheKey = $"{CacheKeys.BEST_STORIES}-{numberOfStories}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<HackerNewsStoryDto> cachedStories))
        {
            try
            {
                var bestStoriesIds = await _httpClient.GetFromJsonAsync<int[]>(Consts.BEST_STORIES_URI);
                var tasks = bestStoriesIds.Take(numberOfStories).Select(id => GetStoryAsync(id));
                var stories = await Task.WhenAll(tasks);
                cachedStories = stories.OrderByDescending(s => s.Score).ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(_cacheDuration);
                _cache.Set(cacheKey, cachedStories, cacheEntryOptions);
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request related errors here
                // Log the exception
                // You can decide to throw, return an empty list, or a default value
                return cachedStories;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception
                return cachedStories;
            }
        }

        return cachedStories;
    }

    private async Task<HackerNewsStoryDto> GetStoryAsync(int id)
    {
        var cacheKey = $"{CacheKeys.GET_STORY}-{id}";
        try
        {
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetAbsoluteExpiration(_cacheDuration);
                var story = await _httpClient.GetFromJsonAsync<HackerNewsStory>($"{Consts.GET_STORY_URI}{id}.json");
                return story?.ToDto();
            });
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP request related errors here
            // Log the exception
            throw; // or handle differently
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            // Log the exception
            throw; // or handle differently
        }
    }
        
}