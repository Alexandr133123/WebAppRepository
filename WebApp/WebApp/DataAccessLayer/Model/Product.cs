using System;
using System.Collections.Generic;
using WebApp.DataAccessLayer.Model;


namespace WebApp.DataAccessLayer.Model
{
    public class Product
    {

        public int PK_ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastModified { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
