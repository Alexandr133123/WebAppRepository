using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.PresentationLayer.DTO
{
    public class ProductGroupByCategory
    {
        public int PK_CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public DateTime LastModified { get; set; }
    }
}
