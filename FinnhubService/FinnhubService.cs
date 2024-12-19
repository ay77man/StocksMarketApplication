using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Text.Json;

namespace  Services
{
    public class FinnhubService : IFinnhubService
    {
        
        private readonly IHttpClientFactory _httpClientFactory;       
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object> GetCompanyProfile(string stockSymbol)
        {
            // Create new client
            using(HttpClient Client = _httpClientFactory.CreateClient())
            {
                // create request object 
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["finnhubtoken"]}")
                };
                //  send request and get response
                HttpResponseMessage httpResponseMessage = Client.Send(httpRequestMessage);
                // read response as stream 
                string responsebody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
                // Deserialize json to dictionary
                Dictionary<string,object>? result = JsonSerializer.Deserialize<Dictionary<string,object>>(responsebody);
                // handle errors 
                if(result == null)
                {
                    throw new InvalidOperationException("NO Response From Server");
                }
                if (result.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(result["error"]));
                }
                return result;
            }
                
        }

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient Client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["finnhubtoken"]}")
                };
                HttpResponseMessage httpResponseMessage = Client.Send(httpRequestMessage);
                string responsebody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
                Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(responsebody);
                if (result == null)
                {
                    throw new InvalidOperationException("NO Response From Server");
                }
                if (result.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(result["error"]));
                }
                return result;
            }
        } 
    }
}
