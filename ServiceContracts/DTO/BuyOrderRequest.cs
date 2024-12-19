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
    /// DTO class represents buy order , can use in inserting - updating
    /// </summary>
    public class BuyOrderRequest  : IValidatableObject
    {
        /// <summary>
        /// The uniqe symbol of the stock
        /// </summary>
        [Required(ErrorMessage = "stockSymbol can't be empty")]
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
        [Range(1, 100000, ErrorMessage = "Quantity must be between 1 , 100000")]
        public uint Quantity { get; set; }
        /// <summary>
        /// the price of the stock 
        /// </summary>
        [Range(1, 10000, ErrorMessage = "Price must be between 1 , 10000")]
        public double Price { get; set; }

        /// <summary>
        ///  to convert BuyOrderRequest to BuyOrder 
        /// </summary>
        /// <returns>Returns BuyOrder Object </returns>
        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder { StockSymbol = StockSymbol, StockName = StockName, Price = Price, DateAndTimeOfOrder = DateAndTimeOfOrder, Quantity = Quantity };
        }

        /// <summary>
        /// Model class-level validation using IValidatableObject
        /// </summary>
        /// <param name="validationContext">ValidationContext to validate</param>
        /// <returns>Returns validation errors as ValidationResult</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           List<ValidationResult> results = new List<ValidationResult>();   
            if(DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
            {
                results.Add(new ValidationResult("DateAndTimeOfOrder can't be older than  Jan 01, 2000 "));
            }
            return results;
        }
    }
}
