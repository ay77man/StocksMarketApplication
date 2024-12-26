using AutoFixture;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Diagnostics;
using System.IO.Pipelines;

namespace StocksTests
{
    public class StocksServiceTests
    {
        private readonly Mock<IStocksRepository> _stockRepositoryMock;
        private readonly IStocksRepository stocksRepository;
        private readonly IStocksService _service;
        private readonly IFixture fixture;
        public StocksServiceTests() 
        {
            fixture = new Fixture();
            _stockRepositoryMock = new Mock<IStocksRepository>();
            stocksRepository = _stockRepositoryMock.Object;
            _service = new StocksService(stocksRepository); 
        }

        #region CreateBuyOrder
        // If Supply BuyOrderRequest null , should ThrowArgumentNullExcptions
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestIsNull_ToBeArgumentNullExcptions()
        {
           // Arrange 
           BuyOrderRequest? buyOrderRequest = null;
           
            //Act
           var action = async () =>
            {
                await _service.CreateBuyOrder(buyOrderRequest);
            };
            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
        // if supply value for quatity less than 1
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_LessThanQuantity_ToBeArgumentExceptio(uint quantity)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp=>temp.Quantity,quantity)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

           //Act
          var action = async () =>
            {
               
               await _service.CreateBuyOrder(buyOrderRequest);
            };
           await action.Should().ThrowAsync<ArgumentException>();

        }
        // if supply value for quatity more than 100000
        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_MoreThanQuantity_ToBeArgumentException(uint quantity)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
     
        // if supply value for price less than 1
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_LessThanPrice_ToBeArgumentException(double price)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, price)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // if supply value for price more than 10000
        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyOrder_MoreThanPrice_ToBeArgumentException(double price)
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, price)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // if supply value for dateandtime older than 2000-01-01  , should throw argumentexception

        [Fact]
        public async Task CreateBuyOrder_OlderThanDateAndTime_ToBeArgumentException()
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions

        [Fact]
        public async Task CreateBuyOrder_StockNameIsNull_ToBeArgumentException()
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockName, null as string)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        { 
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // If Supply Proper , should pass and return BuyOrederResponse
        [Fact]
        public async Task CreateBuyOrder_ProperBuyOrderRequest_ToBesuccessful()
        {  
            // Arrange 
            BuyOrderRequest? buyOrderRequest = fixture.Build<BuyOrderRequest>()
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            //Act
            BuyOrderResponse buyOrderResponse_Acutal = await _service.CreateBuyOrder(buyOrderRequest);

            buyOrder.BuyOrderId = buyOrderResponse_Acutal.BuyOrderId;
            BuyOrderResponse buyOrderResponse_Exepcted = buyOrder.ToBuyOrderResponse();


            // Assert
            buyOrderResponse_Acutal.BuyOrderId.Should().NotBe(Guid.Empty);
            buyOrderResponse_Acutal.Should().Be(buyOrderResponse_Exepcted);
        }

        #endregion

        #region CreateSellOrder

        // If Supply SellOrderRequest null , should ThrowArgumentNullExcptions
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestIsNull_ToBeArgumentNullExeption()
        {
           // Arrange 
           SellOrderRequest? sellOrderRequest = null;
          // Act
           var action = async () =>
           {
              
               await _service.CreateSellOrder(sellOrderRequest);
           };
           //Assert
           await action.Should().ThrowAsync<ArgumentNullException>();

        }
        // if supply value for quatity less than 1
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_LessThanQuantity_ToBeArgumentException(uint quantity)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // if supply value for quatity more than 100000
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_MoreThanQuantity_ToBeArgumentException(uint quantity)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
       

        // if supply value for price less than 1
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_LessThanPrice_ToBeArgumentException(double price)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, price)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // if supply value for price more than 10000
        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_MoreThanPrice_ToBeArgumentException(double price)
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, price)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // if supply value for dateandtime older than 2000-01-01  , should throw argumentexception

        [Fact]
        public async Task CreateSellOrder_OlderThanDateAndTime_ToBeArgumentException()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // If Supply StockName null , should ThrowArgumentExcptions

        [Fact]
        public async Task CreateSellOrder_StockNameIsNull_ToBeArgumentException()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.StockName, null as string)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // If Supply StockSymbol null , should ThrowArgumentExcptions
        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            var action = async () =>
            {

                await _service.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }
        // If Supply Proper , should pass and return SellOrederResponse
        [Fact]
        public async Task CreateSellOrder_ProperBuyOrderRequest()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = fixture.Build<SellOrderRequest>()
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            //Act
            SellOrderResponse sellOrderResponse_Actual = await _service.CreateSellOrder(sellOrderRequest);

            sellOrder.SellOrderId = sellOrderResponse_Actual.SellOrderId;
            SellOrderResponse sellOrderResponse_Expected = sellOrder.ToSellOrderResponse();

            // Assert
            sellOrderResponse_Actual.SellOrderId.Should().NotBe(Guid.Empty);
            sellOrderResponse_Actual.Should().Be(sellOrderResponse_Expected);
        }


        #endregion

        #region GetBuyOrders
        // ByDefault , List Should by Empty
        [Fact]
        public async Task GetBuyOrders_EmptyByDefault()
        {
            // Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();
        
            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrders);

            // Act
            List<BuyOrderResponse> buyOrderResponses_from_Get = await _service.GetBuyOrders();
            //Assert
            buyOrderResponses_from_Get.Should().BeEmpty();
        }
        // If Add Some buy order , should get the same when demand
        [Fact]
        public async Task GetBuyOrder_AddProperDetalis()
        {
            List<BuyOrder> buyOrderResponses = new List<BuyOrder>
            {
                fixture.Create<BuyOrder>(),
                fixture.Create<BuyOrder>(),
                fixture.Create<BuyOrder>()
            };

            List<BuyOrderResponse> buy_order_response_expected = buyOrderResponses.Select(temp => temp.ToBuyOrderResponse()).ToList();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrderResponses);

            //Act 
            List<BuyOrderResponse> buy_order_response_from_get = await _service.GetBuyOrders();

            //Assert
           buy_order_response_from_get.Should().BeEquivalentTo(buy_order_response_expected);
        }
        #endregion

        #region GetSellOrders
        // ByDefault , List Should by Empty
        [Fact]
        public async Task GetSellOrders_EmptyByDefault()
        {
            // Arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrders);

            // Act
            List<SellOrderResponse> sellOrderResponses_from_Get = await _service.GetSellOrders();

            //Assert
            sellOrderResponses_from_Get.Should().BeEmpty();
        }
        // If Add Some Sell order , should get the same when demand
        [Fact]
        public async void GetSellOrder_AddProperDetalis()
        {
            // Arrange
            List<SellOrder> sellOrderResponses = new List<SellOrder>
            {
                fixture.Create<SellOrder>(),
                fixture.Create<SellOrder>(),
                fixture.Create<SellOrder>()
            };

            List<SellOrderResponse> sell_order_response_expected = sellOrderResponses.Select(temp => temp.ToSellOrderResponse()).ToList();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrderResponses);

            //Act 
            List<SellOrderResponse> sell_order_response_from_get = await _service.GetSellOrders();

            //Assert
            sell_order_response_from_get.Should().BeEquivalentTo(sell_order_response_expected);
        }
        #endregion
    }
}