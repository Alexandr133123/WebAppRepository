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
        ProductResponse GetProducts(Filters filters, ProductParameters parameters);
        void UpdateProducts(Product product);
        void AddProduct(Product product,IFormFile uploadedFile);
        void UpdateProducts(Product product, IFormFile uploadedFile);
        void DeleteProduct(int id);
        ChartDTO GetChartChartData();
        byte[] SetProductImage(string relativePath);
    }
}
