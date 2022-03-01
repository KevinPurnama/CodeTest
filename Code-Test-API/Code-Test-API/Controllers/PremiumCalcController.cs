using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerGen;

using Code_Test.Domain;
using Code_Test_API.Models;
using Code_Test.Domain.Models;

namespace Code_Test_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremiumCalcController : ControllerBase
    {
      
        private readonly ILogger<PremiumCalcController> _logger;
        private readonly IPremiumCalcDomain _calculator;
        private readonly IMapper _mapper;

        public PremiumCalcController(ILogger<PremiumCalcController> logger, IPremiumCalcDomain calculator, IMapper mapper)
        {
            _logger = logger;
            _calculator = calculator;
            _mapper = mapper;
        }

        [HttpPost(Name = "CalculateMonthlyPremium")]
        public async Task<MonthlyPremiumResponse> CalculateMonthlyPremium(MonthlyPremiumRequest customerRequest)
        {
            MonthlyPremium calculatedPremium = await _calculator.CalcMonthlyPremiumAsync(_mapper.Map<Customer>(customerRequest));
            MonthlyPremiumResponse response = _mapper.Map<MonthlyPremiumResponse>(calculatedPremium);
            return response;
        }
/*
        [HttpGet(Name = "GetOccupations")]
        public async Task<GetOccupationsResponse> GetOccupationNames()
        {
            return null;
        }
*/
    }
}