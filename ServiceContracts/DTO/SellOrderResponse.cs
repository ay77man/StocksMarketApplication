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
    /// DTO class  represent sell order  ,  use to return type of stock service 
    /// </summary>
    public class SellOrderResponse : IOrderResponse
    {
        /// <summary>
        /// Unige Id of the Sell order 
        /// </summary>
        [Key]
        public Guid SellOrderId { get; set; }
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

        public OrderType TypeOfOrder => OrderType.SellOrder;

        /// <summary>
        /// Override Equals method to compare values not ref
        /// </summary>
        /// <param name="obj"> object to compare with current </param>
        /// <returns>true or false based on mathing of objects </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is SellOrderResponse)) return false;
            SellOrderResponse other = (SellOrderResponse)obj;
            return other.SellOrderId == SellOrderId && other.StockSymbol == StockSymbol && other.Quantity == Quantity && other.Price == Price && other.StockName == StockName && other.DateAndTimeOfOrder == DateAndTimeOfOrder && other.TradeAmount == TradeAmount;
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
            return $" BuyOrderId : {SellOrderId} , StockSymbol : {StockSymbol} , StockName : {StockName} , Price : {Price} , Quantity : {Quantity} , DateAndTimeOfOrder : {DateAndTimeOfOrder} , TradeAmount : {TradeAmount}  ";
        }
    }


    public static class SellOrderExctensions
    {
        /// <summary>
        /// Exctensions method to convert SellOrder to SellOrderResponse
        /// </summary>
        /// <param name="buyOrder">Sellorder object to convert</param>
        /// <returns>Returns SellOrderResponse including newily uinqe SellOrderId</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse { SellOrderId = sellOrder.SellOrderId, Price = sellOrder.Price, StockName = sellOrder.StockName, Quantity = sellOrder.Quantity, StockSymbol = sellOrder.StockSymbol, DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, TradeAmount = sellOrder.Price * sellOrder.Quantity };
        }
    }
}

