using Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string AddressSender { get; set; }
        public string AddressRecipient { get; set; }
        public double Weight { get; set; }
        public DateTime PickupDt { get; set; }
        public DateTime CreatedDt { get; set; }
        public decimal Price { get; set; }
        public string OrderNumber { get; set; }
        public string Truck { get; set; }
    }
}
