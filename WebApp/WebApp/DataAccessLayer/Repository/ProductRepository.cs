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

        public void UpdateProduct(Product product)
        {            
            var productQuery = db.Products.Include(p => p.Categories).Single(dbp => dbp.PK_ProductId == product.PK_ProductId);
            var categoryQuery = db.Categories.Where(dbc => product.Categories.Select(c => c.PK_CategoryId).Contains(dbc.PK_CategoryId)).ToList();
            productQuery.Categories = categoryQuery;
            productQuery.LastModified = DateTime.UtcNow;
            productQuery.Price = product.Price;
            productQuery.ProductName = product.ProductName;
            productQuery.QuantityInStock = product.QuantityInStock;
            db.SaveChanges();         
            
        }

        public void AddProduct(Product product)
        {
            var categoryQuery = db.Categories.Where(dbc => product.Categories.Select(c => c.PK_CategoryId).Contains(dbc.PK_CategoryId)).ToList();
            product.Categories = categoryQuery;
            db.Add(product);
            db.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var product = db.Products.Include(p => p.Categories).Single(p => p.PK_ProductId == id);
            db.Remove(product);
            db.SaveChanges();
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
           .Include(p => p.Categories)
           .AsNoTracking();
            return result;
        }      

    }
}
