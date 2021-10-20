using System;
using System.Collections.Generic;
using WebApp.DataAccessLayer.Model;


namespace WebApp.DataAccessLayer.Model
{
    public class ProductCategory
    {
        public int PfkProductId { get; set; }
        public int PfkCategoryId { get; set; }

        public  Category PfkCategory { get; set; }
        public  Product PfkProduct { get; set; }
    }
}
