using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.PresentationLayer.DTO
{
    public class ProductGroupByLastModified
    {
        public string LastModifiedMonth { get; set; }
        public int ProductCount { get; set; }
        public int AveragePrice { get; set; }
    }
}
