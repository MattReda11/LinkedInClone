using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        }

        public async Task<List<NewsModel>> GetHeadlines()
        {            
            
            string API_KEY = "fcf0c85067c3433ca27e2dd8079fd0b1"; //just trying to get it  working atm, will store more securely later
            var url = string.Format("https://newsapi.org/v2/top-headlines?country=us&apiKey={0}", API_KEY);
            var result = new List<NewsModel>();
            var response = await client.GetAsync(url);
            string debug = $"{response.Headers}, {response.ToString()}";
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();                
                //Console.WriteLine(stringResponse.Substring(0, 1000));
                // result = JsonSerializer.Deserialize<List<NewsModel>>(stringResponse,
                //      new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            }
            else
            {
                Console.WriteLine(debug + "\n URL: " + url);
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        public async Task<string> GetResponseAsString(){
            string API_KEY = "fcf0c85067c3433ca27e2dd8079fd0b1"; //just trying to get it  working atm, will store more securely later
            var url = string.Format("/v2/top-headlines?country=us&category=business&apiKey={0}", API_KEY);
            string result = "";
            var response = await client.GetAsync(url);
            string debug  = $"{response.Headers}, {response.ToString()}";
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result += stringResponse;
                
                // result = JsonSerializer.Deserialize<List<NewsModel>>(stringResponse,
                //      new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            }
            else
            {
                Console.WriteLine("result = " + result);
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}