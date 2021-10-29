using System.Collections.Generic;
using WebApp.PresentationLayer.DTO;

namespace WebApp.PresentationLayer.Wrapper
{
    public class ProductResponse
    {
        public int ProductCount { get; set; }
        public List<ViewProduct> Products { get; set; }
        public decimal MaxProductPrice { get; set; }

        public ProductResponse(List<ViewProduct> products, int productCount, decimal maxProductPrice)
        {
            this.ProductCount = productCount;
            this.Products = products;
            this.MaxProductPrice = maxProductPrice;
        }
    }
}
