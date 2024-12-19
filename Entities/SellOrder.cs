using System;
using System.ComponentModel.DataAnnotations;


namespace Entities
{
    /// <summary>
    /// Represents sell order to sell stock
    /// </summary>
    public class SellOrder
    {
        /// <summary>
        /// The Uniqe Order Id
        /// </summary>
        [Key]     
        public Guid SellOrderId { get; set; }
        /// <summary>
        /// The Uniqe symbol of the stock 
        /// </summary>
        [Required(ErrorMessage = "stockSymbol can't be empty")]
        public string StockSymbol { get; set; }
        /// <summary>
        /// The uniqe name of the stock 
        /// </summary>
        [Required(ErrorMessage = "stockName can't be empty")]
        public string StockName { get; set; }

        /// <summary>
        /// Date and time of order 
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }
        /// <summary>
        /// The Quantity of the stock 
        /// </summary>
        [Range(1, 100000, ErrorMessage = "Quantity must be between 1 , 100000")]
        public uint Quantity { get; set; }
        /// <summary>
        /// The price of the stock
        /// </summary>
        [Range(1, 10000, ErrorMessage = "Price must be between 1 , 10000")]
        public double Price { get; set; }
    }
}
