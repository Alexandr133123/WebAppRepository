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
    public class RepositoryHandler : IRepositoryHandler 
    {
        ApplicationContext db;

        public RepositoryHandler(ApplicationContext context)
        {
            db = context;
        }

       public IEnumerable<Product> GetProduct()
       {
            return db.Products
                   .Include(c => c.ProductCategories).ToList();
       }

    }
}
