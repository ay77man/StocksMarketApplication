using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Diagnostics;

namespace StocksTests
{
    public class StocksServiceTests
    {
        private readonly IStocksService _service;

        public StocksServiceTests() 
        {
            _service = new StocksService(new StocksDBContext(new DbContextOptionsBuilder<StocksDBContext>().Options)); 
        }

        #region CreateBuyOrder
        // If Supply BuyOrderRequest null , should ThrowArgumentNullExcptions
        [Fact]
        public void CreateBuyOrder_BuyOrderRequestIsNull()
        {
           // Arrange 
           BuyOrderRequest? buyOrderRequest = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }
        // if supply value for quatity less than 1
        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_LessThanQuantity(uint quantity)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = 1 , Quantity = quantity,  };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }
        // if supply value for quatity more than 100000
        [Theory]
        [InlineData(100001)]
        public void CreateBuyOrder_MoreThanQuantity(uint quantity)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = quantity, Quantity = quantity, };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }
        // If Supply StockName null , should ThrowArgumentExcptions

        // if supply value for price less than 1
        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_LessThanPrice(double price)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = price, Quantity = 1 };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // if supply value for price more than 10000
        [Theory]
        [InlineData(10001)]
        public void CreateBuyOrder_MoreThanPrice(double price)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = price, Quantity = 1 };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }

        // if supply value for dateandtime older than 2000-01-01  , should throw argumentexception

        [Fact]
        public void CreateBuyOrder_OlderThanDateAndTime()
        {
            // Arrange 
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = 1, Quantity = 1 , DateAndTimeOfOrder = Convert.ToDateTime("1999-01-01") };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions

        [Fact]
        public void CreateBuyOrder_StockNameIsNull()
        {
            // Arrange 
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest { StockName = null };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions
        [Fact]
        public void CreateBuyOrder_StockSymbolIsNull()
        {
            // Arrange 
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest { StockSymbol = null };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateBuyOrder(buyOrderRequest);
            });
        }
        // If Supply Proper , should pass and return BuyOrederResponse
        [Fact]
        public void CreateBuyOrder_ProperBuyOrderRequest()
        {
            // Arrange 
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest { StockName = "Microsoft" , StockSymbol = "AAPL" , Price = 450 , Quantity = 14 , DateAndTimeOfOrder = Convert.ToDateTime("2005-01-01") };
            //Act
            BuyOrderResponse buyOrderResponseFromCreate = _service.CreateBuyOrder(buyOrderRequest);
            //Assert
            Assert.True(buyOrderResponseFromCreate.BuyOrderId != Guid.Empty);
        }

        #endregion

        #region CreateSellOrder

        // If Supply SellOrderRequest null , should ThrowArgumentNullExcptions
        [Fact]
        public void CreateSellOrder_SellOrderRequestIsNull()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }
        // if supply value for quatity less than 1
        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_LessThanQuantity(uint quantity)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = 1, Quantity = quantity, };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }
        // if supply value for quatity more than 100000
        [Theory]
        [InlineData(100001)]
        public void CreateSellOrder_MoreThanQuantity(uint quantity)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = quantity, Quantity = quantity, };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }
       

        // if supply value for price less than 1
        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_LessThanPrice(double price)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = price, Quantity = 1 };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        // if supply value for price more than 10000
        [Theory]
        [InlineData(10001)]
        public void CreateSellOrder_MoreThanPrice(double price)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = price, Quantity = 1 };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }

        // if supply value for dateandtime older than 2000-01-01  , should throw argumentexception

        [Fact]
        public void CreateSellOrder_OlderThanDateAndTime()
        {
            // Arrange 
            SellOrderRequest sellOrderRequest = new SellOrderRequest { StockName = "Microsoft", StockSymbol = "MSFT", Price = 1, Quantity = 1, DateAndTimeOfOrder = Convert.ToDateTime("1999-01-01") };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions

        [Fact]
        public void CreateSellOrder_StockNameIsNull()
        {
            // Arrange 
            SellOrderRequest sellOrderRequest = new SellOrderRequest { StockName = null };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions
        [Fact]
        public void CreateSellOrder_StockSymbolIsNull()
        {
            // Arrange 
            SellOrderRequest sellOrderRequest = new SellOrderRequest { StockSymbol = null };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _service.CreateSellOrder(sellOrderRequest);
            });
        }
        // If Supply Proper , should pass and return SellOrederResponse
        [Fact]
        public void CreateSellOrder_ProperBuyOrderRequest()
        {
            // Arrange 
            SellOrderRequest sellOrderRequest = new SellOrderRequest { StockName = "Microsoft", StockSymbol = "AAPL", Price = 450, Quantity = 14, DateAndTimeOfOrder = Convert.ToDateTime("2005-01-01") };
            //Act
            SellOrderResponse sellOrderResponseFromCreate = _service.CreateSellOrder(sellOrderRequest);
            //Assert
            Assert.True(sellOrderResponseFromCreate.SellOrderId != Guid.Empty);
        }


        #endregion

        #region GetBuyOrders
        // ByDefault , List Should by Empty
        [Fact]
        public void GetBuyOrders_EmptyByDefault()
        {
            // Act
            List<BuyOrderResponse> buyOrderResponses_from_Get = _service.GetBuyOrders();
            //Assert
            Assert.Empty(buyOrderResponses_from_Get);   
        }
        // If Add Some buy order , should get the same when demand
        [Fact]
        public void GetBuyOrder_AddProperDetalis()
        {
            // Arrange 
            BuyOrderRequest buyOrder_request_1 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            BuyOrderRequest buyOrder_request_2 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            List<BuyOrderRequest> buy_order_request = new List<BuyOrderRequest>() {  buyOrder_request_1 , buyOrder_request_2 };

            List<BuyOrderResponse> buyOrderResponses_from_add = new List<BuyOrderResponse>();

            foreach(BuyOrderRequest buyOrderRequest in buy_order_request)
            {
                BuyOrderResponse orderResponse = _service.CreateBuyOrder(buyOrderRequest);
                buyOrderResponses_from_add.Add(orderResponse);
            }
            //Act 
            List<BuyOrderResponse> buy_order_response_from_get = _service.GetBuyOrders();

            //Assert
            foreach(BuyOrderResponse buyOrder in buyOrderResponses_from_add)
            {
                Assert.Contains(buyOrder, buy_order_response_from_get);
            }
        }
        #endregion

        #region GetSellOrders
        // ByDefault , List Should by Empty
        [Fact]
        public void GetSellOrders_EmptyByDefault()
        {
            // Act
            List<SellOrderResponse> sellOrderResponses_from_Get = _service.GetSellOrders();
            //Assert
            Assert.Empty(sellOrderResponses_from_Get);
        }
        // If Add Some Sell order , should get the same when demand
        [Fact]
        public void GetSellOrder_AddProperDetalis()
        {
            // Arrange 
            SellOrderRequest sellOrder_request_1 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            SellOrderRequest sellOrder_request_2 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            List<SellOrderRequest> sell_order_request = new List<SellOrderRequest>() { sellOrder_request_1, sellOrder_request_2 };

            List<SellOrderResponse> sellOrderResponses_from_add = new List<SellOrderResponse>();

            foreach (SellOrderRequest sellOrderRequest in sell_order_request)
            {
                SellOrderResponse orderResponse = _service.CreateSellOrder(sellOrderRequest) ;
                sellOrderResponses_from_add.Add(orderResponse);
            }
            //Act 
            List<SellOrderResponse> sell_order_response_from_get = _service.GetSellOrders();

            //Assert
            foreach (SellOrderResponse sellOrder in sellOrderResponses_from_add)
            {
                Assert.Contains(sellOrder, sell_order_response_from_get);
            }
        }
        #endregion
    }
}