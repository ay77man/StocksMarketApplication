namespace ServiceContracts
{
    public interface IFinnhubService
    {
        /// <summary>
        /// Returns company details such as company country, currency, exchange.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol </param>
        /// <returns>Returns a dictionary that contains details such as company country, currency, exchange.</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
        /// <summary>
        /// Returns stock price details like current price, change in price.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol </param>
        /// <returns>Returns a dictionary that contains details such as current price, change in price, percentage change </returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
