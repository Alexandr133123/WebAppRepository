using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.PresentationLayer.DTO
{
    public class ViewProductResponse
    {
        public int ProductCount { get; set; }
        public List<ViewProduct> Products { get; set; }
        public decimal MaxProductPrice { get; set; }
        public int PageNumber { get; set; }

        public ViewProductResponse(List<ViewProduct> products, int productCount, decimal maxProductPrice, int PageNumber)
        {
            this.ProductCount = productCount;
            this.Products = products;
            this.MaxProductPrice = maxProductPrice;
            this.PageNumber = PageNumber;
        }
    }
}
