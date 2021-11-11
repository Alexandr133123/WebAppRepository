using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        List<Users> GetAllUsers();
        void CreateUser(string username, string password);
    }
}
