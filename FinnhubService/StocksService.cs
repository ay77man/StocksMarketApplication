using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;

namespace Services
{
    public class StocksService : IStocksService
    {
      private readonly IStocksRepository _stocksRepository;

        public StocksService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
           if(buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidaiton(buyOrderRequest);

           BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
           
            buyOrder.BuyOrderId = Guid.NewGuid();

            await _stocksRepository.CreateBuyOrder(buyOrder);

            return buyOrder.ToBuyOrderResponse();
           

        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidaiton(sellOrderRequest);
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderId = Guid.NewGuid();

            await _stocksRepository.CreateSellOrder(sellOrder);
           
            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();

            return buyOrders
                .Select(temp=>temp.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _stocksRepository.GetSellOrders();

            return sellOrders
                .Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
