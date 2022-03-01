using Moq;
using Xunit;

using Code_Test.Domain;
using Code_Test.Domain.Models;

namespace Code_Test.Test
{
    public class PremiumCalcTests
    {
        private Customer defaultCustomer;
        private PremiumCalcDomain service = new PremiumCalcDomain();

        public PremiumCalcTests()
        {
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
            MonthlyPremium result = service.CalcMonthlyPremiumAsync(defaultCustomer).Result;
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
            MonthlyPremium result = service.CalcMonthlyPremiumAsync(defaultCustomer).Result;
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
            MonthlyPremium result = service.CalcMonthlyPremiumAsync(defaultCustomer).Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(1000/12, result.Premium);
        }
    }
}