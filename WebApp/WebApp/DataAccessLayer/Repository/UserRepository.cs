using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public UserRepository(ApplicationContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.db = db;
            this._userManager = userManager;
            this._singInManager = signInManager;
        }

        public async Task<IdentityUser> GetUserAsync(string username, string password)
        {
            IdentityUser user = null;
            return user;
        }
        public async Task CreateUserAsync(string username, string password)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = username;

            var result = await _userManager.CreateAsync(user, password);
          
        }
    }
}
