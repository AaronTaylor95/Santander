namespace SantanderHackerNewsAPI.Models
{
    public class Consts
    {
        public static string BEST_STORIES_URI = "https://hacker-news.firebaseio.com/v0/beststories.json";
        public static string GET_STORY_URI = "https://hacker-news.firebaseio.com/v0/item/";
    }

    public class CacheKeys
    {
        public static string BEST_STORIES = "best-stories-";
        public static string GET_STORY = "story-";
    }
}
