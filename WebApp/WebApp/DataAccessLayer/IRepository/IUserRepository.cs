using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetUserAsync(string username, string password);
        Task CreateUserAsync(string username, string password);
    }
}
