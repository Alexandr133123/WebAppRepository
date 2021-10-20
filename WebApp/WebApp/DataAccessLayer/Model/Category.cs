using System;
using System.Collections.Generic;
using WebApp.DataAccessLayer.Model;


namespace WebApp.DataAccessLayer.Model
{
    public class Category
    {
        public Category()
        {
            InverseFkParentCategory = new HashSet<Category>();
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int PkCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? FkParentCategoryId { get; set; }

        public  Category FkParentCategory { get; set; }
        public  ICollection<Category> InverseFkParentCategory { get; set; }
        public  ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
