using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;

namespace WebApp.BusinessLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository repository;

        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Category> GetCategories()
        {
            return repository.GetCategories();
        }
    }
}
