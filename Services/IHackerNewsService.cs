public interface IHackerNewsService
{
    Task<IEnumerable<HackerNewsStoryDto>> GetBestStoriesAsync(int numberOfStories);
}
