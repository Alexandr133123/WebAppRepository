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
        decimal GetMaxPrice();
        IQueryable<Product> GetProducts(Filters filters);
        void UpdateProduct(Product product);
        void AddProduct(Product product);
        void DeleteProduct(int id);

    }
}
