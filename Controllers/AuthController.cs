using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Enums;
using BBSK_Psycho.Models;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        public string Login([FromBody] Loginrequest request)
        {
            if (request == default || request.Email == default) return string.Empty;
            
            var roleClaim = new Claim(ClaimTypes.Role, (request.Email == "manager@p.ru" ? Role.Manager : request.Email == "psyh@p.ru"? Role.Psychologist: Role.Client).ToString()); //присвоение ролей (клиент, психолог, менеджер)

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Email), roleClaim };

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
