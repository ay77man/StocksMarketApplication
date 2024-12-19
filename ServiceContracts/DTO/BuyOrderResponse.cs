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
    /// DTO class  represent buy order  ,  use to return type of stock service 
    /// </summary>
    public class BuyOrderResponse : IOrderResponse
    {
        /// <summary>
        /// Unige Id of the buy order 
        /// </summary>
        [Key]
        public Guid BuyOrderId { get; set; }
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
        [Range(1, 100000, ErrorMessage = "Price must be between 1 , 10000")]
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public OrderType TypeOfOrder => OrderType.BuyOrder;

        /// <summary>
        /// Override Qquals method to compare values not ref
        /// </summary>
        /// <param name="obj"> object to compare with current </param>
        /// <returns>true or false based on mathing of objects </returns>
        public override bool Equals(object? obj)
        {
            if(obj == null || !(obj is BuyOrderResponse)) return false;
            BuyOrderResponse other = (BuyOrderResponse)obj;
            return other.BuyOrderId == BuyOrderId && other.StockSymbol == StockSymbol && other.Quantity == Quantity && other.Price == Price  && other.StockName == StockName && other.DateAndTimeOfOrder == DateAndTimeOfOrder && other.TradeAmount == TradeAmount;
        }
        /// <summary>
        /// Returns an int value that represents unique stock id of the current object
        /// </summary>
        /// <returns>unique int value</returns>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Converts the current object into string which includes the values of all properties
        /// </summary>
        /// <returns>A string with values of all properties of current object</returns>
        public override string ToString()
        {
            return $" BuyOrderId : {BuyOrderId} , StockSymbol : {StockSymbol} , StockName : {StockName} , Price : {Price} , Quantity : {Quantity} , DateAndTimeOfOrder : {DateAndTimeOfOrder} , TradeAmount : {TradeAmount}  ";
        }
    }


    public static class BuyOrderExctensions
    {
        /// <summary>
        /// Exctensions method to convert BuyOrder to BuyOrderResponse
        /// </summary>
        /// <param name="buyOrder">buyorder object to convert</param>
        /// <returns>Returns BuyOrderResponse including newily uinqe BuyOrderId</returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse { BuyOrderId = buyOrder.BuyOrderId, Price = buyOrder.Price, StockName = buyOrder.StockName , Quantity = buyOrder.Quantity , StockSymbol = buyOrder.StockSymbol , DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder , TradeAmount = buyOrder.Price * buyOrder.Quantity };
        }
    }
}
