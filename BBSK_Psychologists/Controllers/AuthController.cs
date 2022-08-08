using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<string> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.GetUserForLogin(request.Email, request.Password);
            
            return await _authService.GetToken(user);
        }
    }
}
