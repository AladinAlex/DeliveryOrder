using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrders();
        Task<IEnumerable<CityDto>> GetCities();
        Task CreateOrder(int AddressSenderId, int AddressRecipientId, string AddressSender, string AddressRecipient, double weigth, DateTime PickupDt);
        Task<IEnumerable<PriceListDto>> GetPriceLists();
        Task<IEnumerable<TruckDto>> GetTrucks();
        Task<OrderDto> GetOrderById(int OrderId);
    }
}
