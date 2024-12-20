using Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
           if(buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidaiton(buyOrderRequest);

           BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
           
            buyOrder.BuyOrderId = Guid.NewGuid();

            _dbContext.BuyOrders.Add(buyOrder);
            await _dbContext.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
           

        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidaiton(sellOrderRequest);
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderId = Guid.NewGuid();
            _dbContext.SellOrders.Add(sellOrder);
            await _dbContext.SaveChangesAsync();
            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _dbContext.BuyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();

            return buyOrders
                .Select(temp=>temp.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _dbContext.SellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();

            return sellOrders
                .Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
