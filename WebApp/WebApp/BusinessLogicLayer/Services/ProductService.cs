using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;
namespace WebApp.BusinessLogicLayer.Services
{
    public class ProductService : IProductService
    {
       private IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public List<Product> GetProducts()
        {
            return repository.GetProducts();
        }

    }
}
