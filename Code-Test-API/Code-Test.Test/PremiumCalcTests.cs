using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

using Code_Test_API.Controllers;
using Code_Test_API.Models;
using Code_Test.Domain;
using Code_Test.Domain.Models;

namespace Code_Test.Test
{
    public class PremiumCalcTest
    {
        private PremiumCalcController _controller;
        private ILogger<PremiumCalcController> _logger;
        private IMapper _mapper;

        public PremiumCalcTest()
        {
            _logger = new Mock<ILogger<PremiumCalcController>>().Object;
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Code_Test_API.PremiumCalculatorMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void TestPost_CalculateMonthlyPremium()
        {
            MonthlyPremiumRequest request = new MonthlyPremiumRequest()
            {
                Name = "Kevin",
                Age = 39,
                DateOfBirth = new System.DateTime(1983, 3, 2),
                Occupation = "author",
                DeathBenefit = 10
            };
            var handler = new Mock<IPremiumCalcDomain>();
            handler.Setup(domain => domain.CalcMonthlyPremiumAsync(
                It.IsAny<Customer>()))
                   .Returns(() => Task.FromResult(
                        new MonthlyPremium()
                        {
                            Premium = 42,
                            Errors = new List<string>()
                        }))
                   .Verifiable();
            IPremiumCalcDomain service = handler.Object;

            _controller = new PremiumCalcController(_logger, service, _mapper);

            MonthlyPremiumResponse result = _controller.CalculateMonthlyPremium(request).Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(42, result.Premium);
        }

        [Fact]
        public void TestGet_OccupationNames()
        {
            var handler = new Mock<IPremiumCalcDomain>();
            handler.Setup(domain => domain.GetOccupationsAsync())
                   .Returns(() => Task.FromResult(
                        new Occupations()
                        {
                            OccupationNames = new List<string>()
                            {
                                "author",
                                "farmer",
                                "programmer"
                            },
                            Errors = new List<string>()
                        }))
                   .Verifiable();
            IPremiumCalcDomain service = handler.Object;

            _controller = new PremiumCalcController(_logger, service, _mapper);

            GetOccupationsResponse result = _controller.GetOccupationNames().Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(3, result.Occupations.Count);
            Assert.True(result.Occupations.Contains("author"));
            Assert.True(result.Occupations.Contains("farmer"));
            Assert.True(result.Occupations.Contains("programmer"));
            Assert.False(result.Occupations.Contains("florist"));
        }
    }
}
