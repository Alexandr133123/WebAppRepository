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
            var products = query
                .Skip((parameters.PageNumber * parameters.PageSize))
                .Take(parameters.PageSize).ToList();

            if(products.Count() == 0 && parameters.PageNumber > 0)
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
        public void AddProduct(Product product)
        {
            repository.AddProduct(product);
        }
        public void DeleteProduct(int id)
        {
            repository.DeleteProduct(id);
        }
        public ChartDTO GetChartChartData()
        {
            var products = repository.GetProducts().ToList();
            List<decimal> productPrice = new List<decimal>();
            List<int> productCount = new List<int>();
            foreach(Product p in products)
            {
                if (!productPrice.Contains(p.Price))
                {
                    productPrice.Add(p.Price);           
                }
            }
            productPrice.Sort();
            foreach(decimal price in productPrice)
            {
                var currentPriceProductCount = products.Where(ap => ap.Price == price).Select(ap => ap.QuantityInStock);

                productCount.Add(currentPriceProductCount.Sum());
            }
            return new ChartDTO(productPrice, productCount);
        }
        
    }
}
