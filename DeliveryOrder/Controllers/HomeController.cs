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

        public IActionResult CreateOrder()
        {
            return View();
        }

        public IActionResult PriceList()
        {
            return View();
        }

        public IActionResult City()
        {
            return View();
        }
        
        public IActionResult Truck()
        {
            return View();
        }

        public IActionResult Order(int OrderId)
        {
            return View();
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