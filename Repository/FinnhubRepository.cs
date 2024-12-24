using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Text.Json;

namespace Repository
{
    public class FinnhubRepository : IFinnhubRepository
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            // Create new client
            using (HttpClient Client = _httpClientFactory.CreateClient())
            {
                // create request object 
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["finnhubtoken"]}")
                };
                //  send request and get response
                HttpResponseMessage httpResponseMessage = await Client.SendAsync(httpRequestMessage);
                // read response as stream 
                string responsebody = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync()).ReadToEnd();
                // Deserialize json to dictionary
                Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(responsebody);
                // handle errors 
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

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient Client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["finnhubtoken"]}")
                };
                HttpResponseMessage httpResponseMessage = await Client.SendAsync(httpRequestMessage);
                string responsebody = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync()).ReadToEnd();
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

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            //create http client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //create http request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["finnhubtoken"]}") //URI includes the secret token
            };

            //send request
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            //read response body
            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            //convert response body (from JSON into Dictionary)
            List<Dictionary<string, string>>? responseDictionary = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            //return response dictionary back to the caller
            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            //create http client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //create http request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["finnhubtoken"]}") //URI includes the secret token
            };

            //send request
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            //read response body
            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            //convert response body (from JSON into Dictionary)
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            //return response dictionary back to the caller
            return responseDictionary;
        }
    }
}
