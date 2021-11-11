using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.BusinessLogicLayer.IServices
{
    public interface IAuthService
    {
        string GetJWT(string username, string password);
        int GetJWTLifeTime();
        void CreateUser(string username, string password);
    }
}
