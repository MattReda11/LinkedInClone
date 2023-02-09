public class NewsResponse
{
    //this class represents the entire JSON response of the NewsAPI
    public string status { get; set; }
    public int totalResults { get; set; }
    public List<NewsModel> articles { get; set; }
}