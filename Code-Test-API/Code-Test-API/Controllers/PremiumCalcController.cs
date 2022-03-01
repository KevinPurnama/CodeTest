using Microsoft.AspNetCore.Mvc;

using Code_Test.Domain;
using Code_Test_API.Models;

namespace Code_Test_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumCalcController : ControllerBase
    {
      
        private readonly ILogger<PremiumCalcController> _logger;
        private readonly IPremiumCalcDomain _calculator;

        public PremiumCalcController(ILogger<PremiumCalcController> logger, IPremiumCalcDomain calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        [HttpGet(Name = "CalculateMonthlyPremium")]
        public MonthlyPremiumResponse CalculateMonthlyPremium(MonthlyPremiumRequest customerRequest)
        {
            return null;
        }

        [HttpGet(Name = "GetOccupations")]
        public IEnumerable<string> GetOccupationNames()
        {
            return null;
        }
    }
}