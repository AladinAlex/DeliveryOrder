using Application.Interfaces;
using Application.Services;
using DeliveryOrder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeliveryOrder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetOrders();
            return View(orders);
        }

        public async Task<IActionResult> CreateOrder()
        {
            var model = new AddOrder();
            var ct = await _orderService.GetCities();
            model.Cities = ct.ToList();
            return View(model);
        }

        public async Task<IActionResult> PriceList()
        {
            var model = new PriceListModel();
            var pls = await _orderService.GetPriceLists();
            model.priceLists = pls.ToList();
            return View(model);
        }

        public async Task<IActionResult> City()
        {
            var model = new CityModel();
            var cities = await _orderService.GetCities();
            model.cities = cities.ToList();
            return View(model);
        }

        public async Task<IActionResult> Truck()
        {
            var model = new TruckModel();
            var trs = await _orderService.GetTrucks();
            model.Trucks = trs.ToList();
            return View(model);
        }

        public async Task<IActionResult> Order(int OrderId)
        {
            var model = new OrderModel();
            var order = await _orderService.GetOrderById(OrderId);
            model.Order = order;
            return View(model);
        }

        public async Task<IActionResult> AddOrder(AddOrder model)
        {
            await _orderService.CreateOrder(model.AddressSenderId, model.AddressRecipientId, model.AddressSender, model.AddressRecipient, model.Weight, model.PickupDt);
            return RedirectToAction("Orders");
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}