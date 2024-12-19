namespace StocksMarket
{
    /// <summary>
    /// Options Pattern For Stocks Price Configuration. 
    /// </summary>
    public class TradingOptions
    {
        public string? DefaultStockSymbol { get; set; }
        public uint? DefaultOrderQuantity { get; set; }
    }
}
