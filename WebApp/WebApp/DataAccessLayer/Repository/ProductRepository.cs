using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DataAccessLayer.Repository
{
    public class ProductRepository : IProductRepository 
    {
        private ApplicationContext db;

        public ProductRepository(ApplicationContext context)
        {
            db = context;
        }

       public List<Product> GetProducts()
       {
            return db.Products
                   .Include(c => c.ProductCategories).ToList();
       }

    }
}
