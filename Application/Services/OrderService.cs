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
    }
}
