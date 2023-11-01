using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class Address
    {
        public int Id { get; set; }
        public City сity { get; set; }
        public string address { get; set; }
        //public string Street { get; set; }
        //public string Build { get; set; }
        //public string Apartment { get; set; }
    }
}
