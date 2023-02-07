using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkedInClone.Services
{
    public class NewsAPIService : INewsAPIService
    {
        private static readonly HttpClient client;

        static NewsAPIService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://newsapi.org")
            };
        }

        public async Task<List<NewsModel>> GetHeadlines()
        {
            string API_KEY = "86bc0ca4765341e69b69ef2c2cd1428b"; //just trying to get it  working atm, will store more securely later
            var url = string.Format("/v2/top-headlines?country=us&category=business&apiKey={0}",API_KEY );
            var result = new List<NewsModel>();
            var response = await client.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(stringResponse.Substring(0, 1000));
                result = JsonSerializer.Deserialize<List<NewsModel>>(stringResponse,
                     new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            }
            else
            {
                Console.WriteLine("respone: " + response + "\n URL: " + url);
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}