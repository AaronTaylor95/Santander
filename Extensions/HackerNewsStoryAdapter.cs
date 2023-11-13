namespace SantanderHackerNewsAPI.Extensions
{
    public static class HackerNewsStoryAdapter
    {
        public static HackerNewsStoryDto ToDto(this HackerNewsStory response) => new HackerNewsStoryDto
        {
            Title = response.Title,
            Uri = response.Uri,
            PostedBy = response.PostedBy,
            Time = DateTimeOffset.FromUnixTimeMilliseconds(response.Time).DateTime,
            Score = response.Score,
            CommentCount = response.CommentCount
        };
    }
}
