using Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Domain.Order.Order>> GetOrders();
        Task<IEnumerable<City>> GetCities();
        Task CreateOrder(int AddressSenderId, int AddressRecipientId, string AddressSender, string AddressRecipient, double weigth, DateTime PickupDt);
        Task<IEnumerable<PriceList>> GetPriceLists();
        Task<IEnumerable<Truck>> GetTrucks();
        Task<Domain.Order.Order> GetOrderById(int OrderId);
    }
}
