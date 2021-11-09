using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;
namespace WebApp.BusinessLogicLayer.IServices
{
    public interface IProductService
    {
        Task<ProductResponse> GetProductsAsync(Filters filters, ProductParameters parameters);
        void UpdateProducts(Product product);
        Task AddProductAsync(Product product,IFormFile uploadedFile);
        void AddProduct(Product product);
        Task UpdateProductsAsync(Product product, IFormFile uploadedFile);
        void DeleteProduct(int id);
        Task<ChartDTO> GetChartChartDataAsync();
        Task<byte[]> SetProductImageAsync(string relativePath);
    }
}
