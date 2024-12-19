using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;

namespace Services
{
    public class StocksService : IStocksService
    {
      private readonly StocksDBContext _dbContext;

        public StocksService(StocksDBContext stocksDBContext)
        {
            _dbContext = stocksDBContext;
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
           if(buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidaiton(buyOrderRequest);
           BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
           
            buyOrder.BuyOrderId = Guid.NewGuid();
            _dbContext.BuyOrders.Add(buyOrder);
            _dbContext.SaveChanges();
            return buyOrder.ToBuyOrderResponse();
           

        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidaiton(sellOrderRequest);
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderId = Guid.NewGuid();
            _dbContext.SellOrders.Add(sellOrder);
            _dbContext.SaveChanges();
            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = _dbContext.BuyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            return buyOrders
                .Select(temp=>temp.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            List<SellOrder> sellOrders = _dbContext.SellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            return sellOrders
                .Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
