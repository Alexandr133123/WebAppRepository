using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;
using System.Data;
using System.Threading.Tasks;
using System.Collections;

namespace WebApp.DataAccessLayer.Repository
{
    public class ProductRepository : IProductRepository 
    {
        private ApplicationContext db;
        private ICategoryRepository categoryRepository;

        public ProductRepository(ApplicationContext db, ICategoryRepository categoryRepository)
        {
            this.db = db;
            this.categoryRepository = categoryRepository;
           
        }

       public IQueryable<Product> GetProducts()
       {
            return db.Products
                   .Include(c => c.Categories)
                   .AsNoTracking();

       }


        public IQueryable<Category> GetCategories()
        { 
            return db.Categories.AsNoTracking();
        }

        public IQueryable<Product> GetProducts(Filters filters)
        {
            IQueryable<Product> query = db.Products;

            if (filters.PriceFrom.HasValue && filters.PriceTo.HasValue && !(filters.PriceFrom == 0 && filters.PriceTo == 0) && filters.PriceFrom <= filters.PriceTo)
            {
                query = query.Where(p => p.Price >= filters.PriceFrom && p.Price <= filters.PriceTo);
            }

            if (filters.IncludeOutOfStock == true)
            {
                query = query.Where(p => p.QuantityInStock >= 0);
            }
            else 
            {
                query = query.Where(p => p.QuantityInStock > 0);
            }

            if(filters.ProductName != null && filters.ProductName.Length != 0)
            {
                query = query.Where(p => p.ProductName.Contains(filters.ProductName));
            }

            if(filters.Categores != null && filters.Categores.Count != 0)
            {
                query = query.Where(p => p.Categories.Any(c => categoryRepository.GetCategories(filters.Categores).AsNoTracking().ToList().Contains(c)));  
            }

            return query;                
        }

    }
}
