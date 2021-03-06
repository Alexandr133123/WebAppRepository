using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.IRepository
{
    public interface ICategoryRepository
    {

        IQueryable<Category> GetCategories(List<string> categories);
        IEnumerable<Category> GetCategories();


    }
}
