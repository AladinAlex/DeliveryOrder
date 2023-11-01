using Database.Context;
using Domain.Interfaces;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfContext _context;
        public OrderRepository(EfContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _context.Orders.Include(x => x.AddressSender).ThenInclude(x => x.сity)
                .Include(x => x.AddressRecipient).ThenInclude(x => x.сity)
                .Include(x => x.Truck)
                .AsSingleQuery().ToListAsync();
            return orders;
        }
    }
}
