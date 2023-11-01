using Dapper;
using Database.Context;
using Domain.Interfaces;
using Domain.Order;
using Domain.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfContext _context;
        readonly IDbConnection _dbConnection;
        public OrderRepository(EfContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _context.Orders.AsNoTracking().Include(x => x.AddressSender).ThenInclude(x => x.сity)
                .Include(x => x.AddressRecipient).ThenInclude(x => x.сity)
                .Include(x => x.Truck)
                .AsSingleQuery().ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            var cities = await _context.Cities.AsNoTracking().ToArrayAsync();
            return cities;
        }

        public async Task CreateOrder(int AddressSenderId, int AddressRecipientId, string AddressSender, string AddressRecipient, double weigth, DateTime PickupDt)
        {

            // 0. Проверки
            if (AddressSenderId == AddressRecipientId || AddressSender == AddressRecipient)
                return;

            // 1. получаем города
            var citySender = await _context.Cities.FirstAsync(x => x.Id == AddressSenderId);
            var cityRecipient = await _context.Cities.FirstAsync(x => x.Id == AddressRecipientId);

            // 2. находим или создаем адреса
            var adrSender = await _context.Addresses.FirstOrDefaultAsync(x => x.address == AddressSender);
            var adrRecipient = await _context.Addresses.FirstOrDefaultAsync(x => x.address == AddressRecipient);
            if (adrSender == null || adrRecipient == null)
            {
                if (adrSender == null)
                {
                    adrSender = new Address()
                    {
                        address = AddressSender,
                        сity = citySender
                    };
                    await _context.Addresses.AddAsync(adrSender);
                }
                if (adrRecipient == null)
                {
                    adrRecipient = new Address()
                    {
                        address = AddressRecipient,
                        сity = cityRecipient
                    };
                    await _context.Addresses.AddAsync(adrRecipient);
                }
                await _context.SaveChangesAsync();
            }

            // 3. Находим цену между городами и устанавливаем ее для заказа (нужно чтобы если поменяли цену между города, то история сохранилась в заказе)
            var priceList = await _context.PriceLists.FirstAsync(x => x.CityFinish == cityRecipient && x.CityStart == citySender);

            // 4. Генерируем уникальный номер, проверяем на уникальность
            string orderNumber = string.Empty;
            while (true)
            {
                orderNumber = NumberGenerator.Generate(8);
                var ordr = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNumber == orderNumber);
                if (ordr == null)
                    break;
            }

            // 5. Поиск трака.
            string command = $"exec [order].GetFreeTruckId @weight = {weigth}";
            var truck = await _dbConnection.QueryFirstAsync<Truck?>(command);
            if (truck == null || truck.Id == 0)
                return;

            var order = new Order()
            {
                AddressSender = adrSender,
                AddressRecipient = adrRecipient,
                Weight = weigth,
                PickupDt = PickupDt,
                CreatedDt = DateTime.Now,
                Price = priceList.Price,
                OrderNumber = orderNumber,
                //Truck = truck,
                TruckId = truck.Id,
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PriceList>> GetPriceLists()
        {
            var pl = await _context.PriceLists.AsNoTracking().Include(x => x.CityStart).Include(x => x.CityFinish).ToListAsync();
            return pl;
        }

        public async Task<IEnumerable<Truck>> GetTrucks()
        {
            var trs = await _context.Trucks.AsNoTracking().ToListAsync();
            return trs;
        }

        public async Task<Order> GetOrderById(int OrderId)
        {
            var order = await _context.Orders.AsNoTracking().Include(x => x.AddressSender).ThenInclude(x => x.сity)
                .Include(x => x.AddressRecipient).ThenInclude(x => x.сity)
                .Include(x => x.Truck)
                .AsSingleQuery().FirstAsync(x => x.Id == OrderId);
            return order;
        }
    }
}
