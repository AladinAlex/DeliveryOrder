using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRep;
        readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRep, IMapper mapper)
        {
            _orderRep = orderRep;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = await _orderRep.GetOrders();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<CityDto>> GetCities()
        {
            var cities = await _orderRep.GetCities();

            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task CreateOrder(int AddressSenderId, int AddressRecipientId, string AddressSender, string AddressRecipient, double weigth, DateTime PickupDt)
        {
            await _orderRep.CreateOrder(AddressSenderId, AddressRecipientId, AddressSender, AddressRecipient, weigth, PickupDt);
        }

        public async Task<IEnumerable<PriceListDto>> GetPriceLists()
        {
            var pl = await _orderRep.GetPriceLists();
            return _mapper.Map<IEnumerable<PriceListDto>>(pl);
        }

        public async Task<IEnumerable<TruckDto>> GetTrucks()
        {
            var trs = await _orderRep.GetTrucks();
            return _mapper.Map<IEnumerable<TruckDto>>(trs);
        }

        public async Task<OrderDto> GetOrderById(int OrderId)
        {
            var order = await _orderRep.GetOrderById(OrderId);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
