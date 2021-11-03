using System.Collections.Generic;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.PresentationLayer.DTO
{
    public class ProductResponse
    {
        public int ProductCount { get; set; }
        public List<Product> Products { get; set; }
        public decimal MaxProductPrice { get; set; }
        public int pageNumber { get; set; }
        public ProductResponse(List<Product> products, int productCount, decimal maxProductPrice, int pageNumber)
        {
            this.ProductCount = productCount;
            this.Products = products;
            this.MaxProductPrice = maxProductPrice;
            this.pageNumber = pageNumber;
        }
    }
}
