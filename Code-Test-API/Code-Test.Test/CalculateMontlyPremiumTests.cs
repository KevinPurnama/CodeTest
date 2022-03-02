using Microsoft.EntityFrameworkCore;
using Xunit;

using Code_Test.Domain;
using Code_Test.Domain.Models;

namespace Code_Test.Test
{
    public class CalculateMontlyPremiumTests
    {
        private Customer defaultCustomer;
        private PremiumCalcDomain _service;
        private FactorDBContext _factorDb;
        public CalculateMontlyPremiumTests()
        {
            DbContextOptionsBuilder<FactorDBContext> opt = new DbContextOptionsBuilder<FactorDBContext>();
            opt.UseInMemoryDatabase("FactorDBPremiumCalculationTest");
            _factorDb = new FactorDBContext(opt.Options);
            _service = new PremiumCalcDomain(_factorDb);

            defaultCustomer = new Customer()
            {
                Name = "Kevin Purnama",
                Age = 39,
                DateOfBirth = new System.DateTime(1983, 3, 2),
                Occupation = "doctor",
                DeathBenefit = 10000
            };
        }

        [Fact]
        public void TestCalculate_39yoDoctor_DeathBenefit10000()
        {
            MonthlyPremium result = _service.CalcMonthlyPremiumAsync(defaultCustomer).Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(32.5, result.Premium);
        }

        [Fact]
        public void TestCalculate_18yoCleaner_DeathBenefit5000()
        {
            defaultCustomer.Age = 18;
            defaultCustomer.Occupation = "cleaner";
            defaultCustomer.DeathBenefit = 5000;
            MonthlyPremium result = _service.CalcMonthlyPremiumAsync(defaultCustomer).Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(11.25, result.Premium);
        }

        [Fact]
        public void TestCalculate_40yoAuthor_DeathBenefit20000()
        {
            defaultCustomer.Age = 40;
            defaultCustomer.Occupation = "author";
            defaultCustomer.DeathBenefit = 20000;
            MonthlyPremium result = _service.CalcMonthlyPremiumAsync(defaultCustomer).Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(1000/12.0, result.Premium);
        }
    }
}