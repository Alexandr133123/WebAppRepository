using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.BusinessLogicLayer.IServices
{
    public interface IAuthService
    {
        Task<string> GetJWTAsync(string username, string password);
        int GetJWTLifeTime();
        Task CreateUserAsync(string username, string password);
    }
}
