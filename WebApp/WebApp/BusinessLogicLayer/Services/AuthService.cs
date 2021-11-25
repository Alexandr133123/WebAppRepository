using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.BusinessLogicLayer.Services
{
    public class AuthService: IAuthService
    {
        private IUserRepository repository;
        private IConfiguration configuration;

        public AuthService(IUserRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<string> GetJWTAsync(string username, string password)
        {
            AuthOptions authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return null;
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(authOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public int GetJWTLifeTime()
        {
            int lifeTime = configuration.GetSection("AuthOptions:LifeTime").Get<int>();
            return lifeTime;
        }
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            IdentityUser user = await repository.GetUserAsync(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
        public async Task CreateUserAsync(string username, string password)
        {
           await repository.CreateUserAsync(username, password);
        }
    }
}
