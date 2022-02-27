using Microsoft.AspNetCore.Mvc;

namespace Code_Test_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumCalcController : ControllerBase
    {
      
        private readonly ILogger<PremiumCalcController> _logger;

        public PremiumCalcController(ILogger<PremiumCalcController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOccupations")]
        public IEnumerable<string> Get()
        {
            return null;
        }
    }
}