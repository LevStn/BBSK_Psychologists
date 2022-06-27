using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Enums;
using BBSK_Psycho.Models.Requests;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        public string Login([FromBody] UserLoginrequest request)
        {
            if (request == default || request.Email == default) return string.Empty;

            var roleClaim = new Claim(ClaimTypes.Role, (request.Email == "q@qq.qq" ? Role.Manager : Role.Client).ToString()); //присвоение ролей (клиент, психолог, менеджер)

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Email), roleClaim };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1440)), // время действия - 1 сутки
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
