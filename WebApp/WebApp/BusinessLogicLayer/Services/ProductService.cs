using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.BusinessLogicLayer.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository repository;
        private string imageStoragePath;
        private IConfiguration config;
        public ProductService(IProductRepository repository, IConfiguration config)
        {
            this.repository = repository;
            this.config = config;
            imageStoragePath = config["ImagePath"];
            Directory.CreateDirectory(imageStoragePath);

        }

        public async Task<byte[]> SetProductImageAsync(string relativePath)
        {
            if (!String.IsNullOrWhiteSpace(relativePath))
            {
                string path = Path.Combine(imageStoragePath, relativePath);

                byte[] arr = await System.IO.File.ReadAllBytesAsync(path);

                return arr;
            }
            return null;

        }
        public async Task<ProductResponse> GetProductsAsync(Filters filters, ProductParameters parameters)
        {
            IQueryable<Product> query = repository.GetProducts(filters);
            int resultCount = await query.CountAsync();
            List<Product> products = await query
                .Skip((parameters.PageNumber * parameters.PageSize))
                .Take(parameters.PageSize).ToListAsync();

            if (products.Count() == 0 && parameters.PageNumber > 0)
            {
                products = await query
                .Take(parameters.PageSize).ToListAsync();
                parameters.PageNumber = 0;
            }
            decimal maxPrice = await repository.GetMaxPrice();

            return (new ProductResponse(products, resultCount, maxPrice, parameters.PageNumber));
        }
        public void UpdateProducts(Product product)
        {
            repository.UpdateProduct(product);

        }
        public async Task UpdateProductsAsync(Product product, IFormFile uploadedFile)
        {
            await repository.UpdateProduct(product, uploadedFile);
            await CreateUploadedFileAsync(uploadedFile);
        }
        private async Task CreateUploadedFileAsync(IFormFile uploadedFile)
        {
            string path = imageStoragePath + uploadedFile.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
        }
        public void AddProduct(Product product)
        {
            repository.AddProduct(product);
        }
        public async Task AddProductAsync(Product product, IFormFile uploadedFile)
        {           
            await repository.AddProduct(product, uploadedFile);
            await CreateUploadedFileAsync(uploadedFile);
        }
        public void DeleteProduct(int id)
        {
            repository.DeleteProductAsync(id);
        }
        public async Task<ChartDTO> GetChartChartDataAsync()
        {
            List<Product> products = await repository.GetProducts().ToListAsync();
            List<decimal> productPrice = products.Select(p => p.Price).Distinct().ToList();
            productPrice.Sort();
            List<int> productCount = productPrice
                .Select(price => products.Where(product => product.Price == price).Sum(product => product.QuantityInStock)).ToList();

            return new ChartDTO(productPrice, productCount);
        }

    }
}
