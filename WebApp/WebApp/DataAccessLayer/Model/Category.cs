using System;
using System.Collections.Generic;
using WebApp.DataAccessLayer.Model;


namespace WebApp.DataAccessLayer.Model
{
    public class Category
    {

        public int PK_CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? FK_ParentCategoryId { get; set; }
        public Category FK_ParentCategory { get; set; }
        public ICollection<Category> InverseFkParentCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
