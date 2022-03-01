using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Code_Test.Domain.Models;

namespace Code_Test.Domain
{
    public class PremiumCalcDomain : IPremiumCalcDomain
    {
        const int monthlyDenominator = 12000;

        public List<string> ValidateCustomerData(Customer customerData)
        {
            List<string> errors = new List<string>();
            if (!string.IsNullOrEmpty(customerData.Name))
            {
                Regex regex = new Regex(@"\d");
                if (regex.IsMatch(customerData.Name))
                {
                    errors.Add("Name cannot legally contain digits.");
                }
            }
            if (customerData.Age < 18 || customerData.Age > 110)
            {
                errors.Add("Age must be in the range of 18 to 100.");
            }
            if (customerData.DeathBenefit < 1)
            {
                errors.Add("Death Benefit must be greater than zero.");
            }
            // TODO: add more validation
            return errors;
        }

        public async Task<MonthlyPremium> CalcMonthlyPremiumAsync(Customer customerData)
        {
            try
            {
                var validationErrors = this.ValidateCustomerData(customerData);
                if (validationErrors.Count > 0)
                {
                    return new MonthlyPremium()
                    {
                        Premium = 0,
                        Errors = validationErrors
                    };
                }
                double factor = 1.75;
                // (Death Cover amount * Occupation Rating Factor * Age) /1000 * 12
                double premium = (customerData.DeathBenefit * factor * customerData.Age) / monthlyDenominator;
                return new MonthlyPremium()
                {
                    Premium = premium
                };
            }
            catch (Exception e)
            {
                return new MonthlyPremium()
                {
                    Premium = 0,
                    Errors = new List<string>() {
                        e.Message
                    }
                };
            }

        }

        public async Task<Occupations> GetOccupationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}