using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;


        public ManagerController(ILogger<ManagerController> logger)
        {
            _logger = logger;
        }

        
    }
}
