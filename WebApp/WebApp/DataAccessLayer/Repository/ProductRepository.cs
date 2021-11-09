using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

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

        public async Task UpdateProduct(Product product)
        {            
            Product lProduct = await db.Products.Include(p => p.Categories).SingleAsync(dbp => dbp.PK_ProductId == product.PK_ProductId);
            List<Category> categoryList = await db.Categories.Where(dbc => product.Categories.Select(c => c.PK_CategoryId).Contains(dbc.PK_CategoryId)).ToListAsync();
            lProduct.Categories = categoryList;
            lProduct.LastModified = DateTime.UtcNow;
            lProduct.Price = product.Price;

            string prevProductName = lProduct.ProductName;
            lProduct.ProductName = product.ProductName;
            lProduct.QuantityInStock = product.QuantityInStock;
            await db.SaveChangesAsync();
            string categoryString = string.Join(", ", lProduct.Categories.Select(c => c.CategoryName));
            string logString = String.Format("Product: {0} Updated =>\nProductName:{1}\nPrice:{2}\nQuantityInStock:{3}\nLastModified:{4}\nCategories:{5}",
                prevProductName, lProduct.ProductName, lProduct.Price, lProduct.QuantityInStock, lProduct.LastModified,categoryString);
            Log.Information(logString);
        }
        public async Task UpdateProduct(Product product, IFormFile uploadedFile)
        {
            Product lProduct = await db.Products.Include(p => p.Categories).Include(p => p.Image).SingleAsync(dbp => dbp.PK_ProductId == product.PK_ProductId);
            if(lProduct.Image == null)
            {
                lProduct.Image = new ProductImage(lProduct.PK_ProductId, uploadedFile.FileName);
            }
            else
            {
                lProduct.Image.ImageRelativePath = uploadedFile.FileName;
            }
            List<Category> categoryList = await db.Categories.Where(dbc => product.Categories.Select(c => c.PK_CategoryId).Contains(dbc.PK_CategoryId)).ToListAsync();
            lProduct.Categories = categoryList;
            lProduct.LastModified = DateTime.UtcNow;
            lProduct.Price = product.Price;

            string prevProductName = lProduct.ProductName;
            lProduct.ProductName = product.ProductName;
            lProduct.QuantityInStock = product.QuantityInStock;
            await db.SaveChangesAsync();
            string categoryString = string.Join(", ", lProduct.Categories.Select(c => c.CategoryName));
            string logString = String.Format("Product: {0} Updated =>\nProductName:{1}\nPrice:{2}\nQuantityInStock:{3}\nLastModified:{4}\nCategories:{5}",
                prevProductName, lProduct.ProductName, lProduct.Price, lProduct.QuantityInStock, lProduct.LastModified, categoryString);
            Log.Information(logString);
        }

        public async Task AddProduct(Product product)
        {
            List<Category> categoryList = await db.Categories.Where(dbc => product.Categories.Select(c => c.PK_CategoryId).Contains(dbc.PK_CategoryId)).ToListAsync();
            product.Categories = categoryList;
            await db.AddAsync(product);
            await db.SaveChangesAsync();          
            string categoryString = string.Join(", ", product.Categories.Select(c => c.CategoryName));
            string logString = String.Format("Product: {0} Added =>\nProductName:{1}\nPrice:{2}\nQuantityInStock:{3}\nLastModified:{4}\nCategories:{5}",
               product.ProductName, product.ProductName, product.Price, product.QuantityInStock, product.LastModified, categoryString);
            Log.Information(logString);
        }
        public async Task AddProduct(Product product, IFormFile uploadedFile)
        {
            List<Category> categoryList = await db.Categories.Where(dbc => product.Categories.Select(c => c.PK_CategoryId).Contains(dbc.PK_CategoryId)).ToListAsync();
            product.Categories = categoryList;
            product.Image = new ProductImage(product.PK_ProductId, uploadedFile.FileName);
            await db.AddAsync(product);
            await db.SaveChangesAsync();
            string categoryString = string.Join(", ", product.Categories.Select(c => c.CategoryName));
            string logString = String.Format("Product: {0} Added =>\nProductName:{1}\nPrice:{2}\nQuantityInStock:{3}\nLastModified:{4}\nCategories:{5}",
               product.ProductName, product.ProductName, product.Price, product.QuantityInStock, product.LastModified, categoryString);
            Log.Information(logString);

        }
        public async Task DeleteProductAsync(int id)
        {
            Product product = await db.Products.Include(p => p.Categories).Include(p => p.Image).SingleAsync(p => p.PK_ProductId == id);           
            db.Remove(product);            
            await db.SaveChangesAsync();
            Log.Information($"Product Deleted => {product.ProductName}");
        }

        public async Task<decimal> GetMaxPrice()
        {
            return await db.Products.MaxAsync(p => p.Price); 
        }
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
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

            IQueryable<Product> result = query
           .Include(p => p.Categories)
           .Include(p => p.Image)
           .AsNoTracking();
            return result;
        }      

    }
}
