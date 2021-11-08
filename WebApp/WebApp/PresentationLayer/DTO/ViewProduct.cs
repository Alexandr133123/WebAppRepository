using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;

namespace WebApp.PresentationLayer.DTO
{
    public class ViewProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public byte[] Image { get; set; }
        public ICollection<ViewCategory> Categories { get; set; }


    }
}
