using AutoMapper;
using System;
using System.Collections.Generic;
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

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;

        }


        public ProductResponse GetProducts(Filters filters, ProductParameters parameters)
        {
            var query = repository.GetProducts(filters);
            var resultCount = query.Count();
            var testQuery = query
                .Skip((parameters.PageNumber * parameters.PageSize))
                .Take(parameters.PageSize).ToList();

            while (testQuery.Count() == 0)
            {
                parameters.PageNumber = parameters.PageNumber - 1;
                testQuery = query
                .Skip((parameters.PageNumber * parameters.PageSize))
                .Take(parameters.PageSize).ToList();

            }         
            decimal maxPrice = repository.GetMaxPrice();

            return (new ProductResponse(testQuery, resultCount, maxPrice, parameters.PageNumber));
        }
        public void UpdateProducts(Product product)
        {
            repository.UpdateProduct(product);

        }
        public void AddProduct(Product product)
        {
            repository.AddProduct(product);
        }
        public void DeleteProduct(int id)
        {
            repository.DeleteProduct(id);
        }
    }
}
