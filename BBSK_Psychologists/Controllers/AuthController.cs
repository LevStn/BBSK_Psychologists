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
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public string Login([FromBody] LoginRequest request)
        {
            var user = _authService.GetUserForLogin(request.Email, request.Password);
            
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), {new Claim(ClaimTypes.Role, user.Role) } };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), // время действия - 1 сутки
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
