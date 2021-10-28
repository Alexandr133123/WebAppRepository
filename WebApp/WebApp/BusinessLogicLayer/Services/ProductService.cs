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

        public IEnumerable<Product> GetProducts()
        {
            return repository.GetProducts();
        }

        public IEnumerable<Product> GetProducts(Filters filters)
        {
            return repository.GetProducts(filters);
        }

    }
}
