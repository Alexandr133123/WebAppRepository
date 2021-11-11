using Microsoft.AspNetCore.Mvc;
using WebApp.BusinessLogicLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebApp.PresentationLayer.DTO;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        IAuthService service;

        public AuthorizationController(IAuthService service)
        {
            this.service = service;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            string token = service.GetJWT(loginModel.Username, loginModel.Password);
            if (token != null)
            {
                setTokenInfo(token);
                return Ok();
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] LoginModel loginModel)
        {
            service.CreateUser(loginModel.Username, loginModel.Password);
            string token = service.GetJWT(loginModel.Username, loginModel.Password);
            if (token != null)
            {
                setTokenInfo(token);
                return Ok();
            }
            return Unauthorized();
        }
        private void setTokenInfo(string token)
        {            
            Response.Cookies.Append("Cookie", token, new CookieOptions
            {

                Expires = (DateTimeOffset.Now + TimeSpan.FromMinutes(service.GetJWTLifeTime())),

            });
        }
    }
}
