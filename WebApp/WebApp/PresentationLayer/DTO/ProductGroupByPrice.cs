using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.PresentationLayer.DTO
{
    public class ProductGroupByPrice
    {
        public string PriceRange { get; set; }
        public int ProductCount { get; set; }
    }
}
