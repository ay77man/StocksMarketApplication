using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO Class that represents sell order , can use in inserting - updating 
    /// </summary>
    public class SellOrderRequest : IValidatableObject
    {

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

        /// <summary>
        ///  To covert SellOrderRequest to SellOrder 
        /// </summary>
        /// <returns> Returns SellOrder object </returns>
        public SellOrder ToSellOrder()
        {
            return new SellOrder { StockName = StockName , StockSymbol = StockSymbol, DateAndTimeOfOrder = DateAndTimeOfOrder , Price = Price , Quantity = Quantity };
        }
        /// <summary>
        /// Model class-level validation using IValidatableObject
        /// </summary>
        /// <param name="validationContext">ValidationContext to validate</param>
        /// <returns>Returns validation errors as ValidationResult</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           List<ValidationResult> results = new List<ValidationResult>();
            if (DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
            {
                results.Add(new ValidationResult("DateAndTimeOfOrder can't be older than Jan 01, 2000"));
            }
            return results;
        }
    }
}
