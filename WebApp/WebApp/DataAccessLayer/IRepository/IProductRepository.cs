using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.DataAccessLayer.IRepository
{
    public interface IProductRepository
    {
        Task<decimal> GetMaxPrice();
        IQueryable<Product> GetProducts(Filters filters);
        Task UpdateProduct(Product product);
        Task UpdateProduct(Product product, IFormFile uploadedFile);
        Task AddProduct(Product product);
        Task AddProduct(Product product, IFormFile uploadedFile);
        Task DeleteProductAsync(int id);
        IQueryable<Product> GetProducts();

    }
}
