using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;


namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public string Login([FromBody] LoginRequest request)
        {
            var user = _authService.GetUserForLogin(request.Email, request.Password);
            
            return  _authService.GetToken(user);
        }
    }
}
