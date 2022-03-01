using System.Collections.Generic;
using Xunit;

using Code_Test.Domain;
using Code_Test.Domain.Models;

namespace Code_Test.Test
{
    public class ValidateCustomerDataTests
    {
        private Customer defaultCustomer;
        private PremiumCalcDomain service = new PremiumCalcDomain();

        public ValidateCustomerDataTests()
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
        public void TestValidate_CustomerNameHasNoDigits()
        {
            // Normal customer name with no digits
            List<string> result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Empty(result);

            defaultCustomer.Name = "Kevin Zero Seven";
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Empty(result);

            defaultCustomer.Name = "Kevin 07";
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Name cannot legally contain digits.", result[0]);

            defaultCustomer.Name = "7";
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Name cannot legally contain digits.", result[0]);

        }

        [Fact]
        public void TestValidate_CustomerHasDeathBenefit()
        {
            // Normal customer death benefit set
            List<string> result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Empty(result);

            defaultCustomer.DeathBenefit = 0;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Death Benefit must be greater than zero.", result[0]);

            defaultCustomer.DeathBenefit = -1;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Death Benefit must be greater than zero.", result[0]);
        }

        [Fact]
        public void TestValidate_CustomerHasAgeInRange()
        {
            // Normal customer age set in range
            List<string> result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Empty(result);

            defaultCustomer.Age = 0;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Age must be in the range of 18 to 100.", result[0]);

            defaultCustomer.Age = -1;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Age must be in the range of 18 to 100.", result[0]);

            defaultCustomer.Age = 17;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Age must be in the range of 18 to 100.", result[0]);

            defaultCustomer.Age = 18;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Empty(result);

            defaultCustomer.Age = 200;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Age must be in the range of 18 to 100.", result[0]);

            defaultCustomer.Age = 110;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Empty(result);

            defaultCustomer.Age = 111;
            result = service.ValidateCustomerData(defaultCustomer);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Age must be in the range of 18 to 100.", result[0]);

        }

    }
}