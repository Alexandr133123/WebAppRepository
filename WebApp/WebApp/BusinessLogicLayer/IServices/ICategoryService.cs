using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;

namespace WebApp.BusinessLogicLayer.IServices
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
