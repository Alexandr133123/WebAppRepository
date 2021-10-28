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
        public virtual  Category FK_ParentCategory { get; set; }
        public virtual ICollection<Category> InverseFkParentCategory { get; set; }
        public virtual  ICollection<Product> Products { get; set; }
    }
}
