using System.Text.Json.Serialization;

public class HackerNewsStory
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("url")]
    public string Uri { get; set; }

    [JsonPropertyName("by")]
    public string PostedBy { get; set; }

    [JsonPropertyName("time")]    
    public int Time { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("descendants")]
    public int CommentCount { get; set; }
}