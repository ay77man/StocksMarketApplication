using ServiceContracts.DTO;

namespace StocksMarket.Models
{
    public class Orders
    {
       public List<BuyOrderResponse> BuyOrders;

       public List<SellOrderResponse> SellOrders;
    }
}
