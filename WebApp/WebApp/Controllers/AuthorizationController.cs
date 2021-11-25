using Microsoft.AspNetCore.Mvc;
using WebApp.BusinessLogicLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebApp.PresentationLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService service;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthorizationController(IAuthService service, SignInManager<IdentityUser> signInManager)
        {
            this.service = service;
            this._signInManager = signInManager;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);
            if (result.Succeeded)
            {                
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] LoginModel loginModel)
        {
            await service.CreateUserAsync(loginModel.Username, loginModel.Password);
   
            return Ok();
        }
        private void setTokenInfo(string token)
        {            
            Response.Cookies.Append("Cookie",token, new CookieOptions
            {

                Expires = (DateTimeOffset.Now + TimeSpan.FromMinutes(service.GetJWTLifeTime())),

            });
        }
    }
}
