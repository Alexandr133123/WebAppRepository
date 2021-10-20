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
        IRepositoryHandler repository;

        public ProductService(IRepositoryHandler repository)
        {
            this.repository = repository;
        }

        public ICollection<Product> GetProductsFromRep()
        {
            return repository.GetProduct().ToList();
        }

    }
}
