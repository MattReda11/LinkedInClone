using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LinkedInClone
{
    public class FinnhubClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FinnhubClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://finnhub.io/api/v1/")
            };
        }

        public async Task<Dictionary<string, Quote>> GetQuotesAsync(string[] symbols)
        {
            var quotes = new Dictionary<string, Quote>();
            foreach (var symbol in symbols)
            {
                var quote = await GetQuoteAsync(symbol);
                quotes[symbol] = quote;
            }

            return quotes;
        }

        public async Task<Quote> GetQuoteAsync(string symbol)
        {
            var response = await _httpClient.GetAsync($"quote?symbol={symbol}&token={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Quote>(content);
        }
    }

    public class Quote
    {
        public decimal O { get; set; }
        public decimal H { get; set; }
        public decimal L { get; set; }
        public decimal C { get; set; }
        public decimal PC { get; set; }
        public long T { get; set; }
    }
}