using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Represents buy order to purchase stock
    /// </summary>
    public class BuyOrder
    {
        /// <summary>
        /// Unige Id of the buy order 
        /// </summary>
        [Key]
       public Guid BuyOrderId { get; set; }
        /// <summary>
        /// The uniqe symbol of the stock
        /// </summary>
        [Required(ErrorMessage ="stockSymbol can't be empty")]
       public string StockSymbol { get; set; }
        /// <summary>
        /// The uniqe name of the stock 
        /// </summary>
        [Required(ErrorMessage = "stockName can't be empty")]
        public string StockName { get; set; }

        /// <summary>
        /// Date and time of the order 
        /// </summary>
       public DateTime DateAndTimeOfOrder { get; set; }
        /// <summary>
        /// The Quantity of the stock
        /// </summary>
        [Range(1,100000,ErrorMessage = "Quantity must be between 1 , 100000")]
       public uint Quantity { get; set; }
        /// <summary>
        /// the price of the stock 
        /// </summary>
        [Range(1, 10000, ErrorMessage = "Price must be between 1 , 10000")]
        public double Price { get; set; }
    }
}
