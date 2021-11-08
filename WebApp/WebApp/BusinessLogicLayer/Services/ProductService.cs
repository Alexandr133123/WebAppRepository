using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public byte[] SetProductImage(string relativePath)
        {
            if (!String.IsNullOrWhiteSpace(relativePath))
            {
                string path = Path.Combine(imageStoragePath, relativePath);

                var arr = System.IO.File.ReadAllBytes(path);

                return arr;
            }
            return null;

        }
        public ProductResponse GetProducts(Filters filters, ProductParameters parameters)
        {
            var query = repository.GetProducts(filters);
            var resultCount = query.Count();
            var products = query
                .Skip((parameters.PageNumber * parameters.PageSize))
                .Take(parameters.PageSize).ToList();

            if (products.Count() == 0 && parameters.PageNumber > 0)
            {
                products = query
                .Take(parameters.PageSize).ToList();
                parameters.PageNumber = 0;
            }
            decimal maxPrice = repository.GetMaxPrice();

            return (new ProductResponse(products, resultCount, maxPrice, parameters.PageNumber));
        }
        public void UpdateProducts(Product product)
        {
            repository.UpdateProduct(product);

        }
        public void UpdateProducts(Product product, IFormFile uploadedFile)
        {
            CreateUploadedFile(uploadedFile);
            repository.UpdateProduct(product, uploadedFile);
        }
        private void CreateUploadedFile(IFormFile uploadedFile)
        {
            string path = imageStoragePath + uploadedFile.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }
        }
        public void AddProduct(Product product, IFormFile uploadedFile)
        {
            CreateUploadedFile(uploadedFile);
            repository.AddProduct(product, uploadedFile);
        }
        public void DeleteProduct(int id)
        {
            repository.DeleteProduct(id);
        }
        public ChartDTO GetChartChartData()
        {
            var products = repository.GetProducts().ToList();
            List<decimal> productPrice = products.Select(p => p.Price).Distinct().ToList();
            productPrice.Sort();
            List<int> productCount = productPrice
                .Select(price => products.Where(product => product.Price == price).Sum(product => product.QuantityInStock)).ToList();

            return new ChartDTO(productPrice, productCount);
        }

    }
}
