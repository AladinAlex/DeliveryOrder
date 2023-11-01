using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class PriceList
    {
        public int Id { get; set; }
        public City CityStart { get; set; }
        public City CityFinish { get; set; }
        public decimal Price { get; set; }
        public int DurationDay { get; set; }
    }
}
