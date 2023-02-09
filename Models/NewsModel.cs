using Newtonsoft.Json;
public class NewsModel
{
    [JsonProperty(PropertyName = "source")]
    public Source source { get; set; }
    [JsonProperty(PropertyName = "author")]
    public string author { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string title { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string description { get; set; }

    [JsonProperty(PropertyName = "url")]
    public string url { get; set; }

    [JsonProperty(PropertyName = "urlToImage")]
    public string urlToImage { get; set; }

    [JsonProperty(PropertyName = "publishedAt")]
    public string publishedAt { get; set; }

    [JsonProperty(PropertyName = "content")]
    public string content { get; set; }
}


public class Source
{
    public string id { get; set; }
    public string name { get; set; }
}

