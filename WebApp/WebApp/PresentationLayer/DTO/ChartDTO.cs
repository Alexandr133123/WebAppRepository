using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.PresentationLayer.DTO
{
    public class ChartDTO
    {
        public List<decimal> ProductPrice { get; set; }
        public List<int> ProductCount { get; set; }

        public ChartDTO(List<decimal> ProductPrice, List<int> ProductCount)
        {
            this.ProductPrice = ProductPrice;
            this.ProductCount = ProductCount;
        }
    }
}
