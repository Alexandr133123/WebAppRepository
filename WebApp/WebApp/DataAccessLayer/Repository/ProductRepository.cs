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
using System;

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

        public decimal GetMaxPrice()
        {
            return db.Products.Max(p => p.Price); 
        }

        public  IQueryable<Product> GetProducts(Filters filters)
        {
            IQueryable<Product> query = db.Products;

            if (filters.PriceTo.HasValue && filters.PriceTo != 0 && filters.PriceFrom < filters.PriceTo)
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

            if (!String.IsNullOrWhiteSpace(filters.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(filters.ProductName));
            }

            if (filters.Categores != null && filters.Categores.Count != 0)
            {
                query = query.Where(p => p.Categories.Any(c => categoryRepository.GetCategories(filters.Categores).AsNoTracking().ToList().Contains(c)));
            }

            var result = query
           .AsNoTracking();
            return result;
        }

    }
}
