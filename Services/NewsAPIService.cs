using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LinkedInClone.Services
{
    public class NewsAPIService : INewsAPIService
    {
        private static HttpClient client;

        static NewsAPIService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://newsapi.org"),
            };
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.Add("User-Agent", "JobLink (ASP.NET LinkedIn Clone)");
        }

        public async Task<NewsResponse> GetHeadlines()
        {
            string API_KEY = "fcf0c85067c3433ca27e2dd8079fd0b1"; //just trying to get it  working atm, will store more securely later
            var url = string.Format("https://newsapi.org/v2/top-headlines?country=us&apiKey={0}", API_KEY);
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(stringResponse);
                        return newsResponse;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"JSON error: " + ex.Message);
                    }
                }
                else
                {
                    Debug.WriteLine($"Response status code: {response.StatusCode}");
                    Debug.WriteLine($"Response content: {await response.Content.ReadAsStringAsync()}");
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }
    }
}