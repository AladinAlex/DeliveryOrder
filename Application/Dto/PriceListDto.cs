using Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PriceListDto
    {
        //public int Id { get; set; }
        public string CityStart { get; set; }
        public string CityFinish { get; set; }
        public decimal Price { get; set; }
        public int DurationDay { get; set; }
    }
}
