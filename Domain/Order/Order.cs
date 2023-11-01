using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class Order
    {
        public int Id { get; set; }
        public Address AddressSender { get; set; }
        public Address AddressRecipient { get; set; }
        public double Weight { get; set; }
        public DateTime PickupDt { get; set; }
        public DateTime CreatedDt { get; set; }
        public decimal Price { get; set; }
        public string OrderNumber { get; set; }
        public Truck Truck { get; set; }
        public int TruckId { get; set; }
        //public int AddressSenderId { get; set; }
        //public int AddressRecipientId { get; set; }
    }
}
