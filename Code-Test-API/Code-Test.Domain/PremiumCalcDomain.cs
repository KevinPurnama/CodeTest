using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Code_Test.Domain.DataModels;
using Code_Test.Domain.Models;

namespace Code_Test.Domain
{
    public class PremiumCalcDomain : IPremiumCalcDomain
    {
        const int monthlyDenominator = 12000;

        private FactorDBContext _dbContext;

        public PremiumCalcDomain(FactorDBContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext != null)
            {
                _dbContext.PopulateData();
            }
        }
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
                double factor = 0;
                /*
                factor = _dbContext.Occupations
                    .Where(o => o.Name == customerData.Occupation.ToLower())
                    .Include(o => o.Rating)
                    .Select(o => o.Rating.Factor).First();
                */
                factor = _dbContext.OccupationRatings
                    .Join(_dbContext.Occupations, r => r.Rating, o => o.Rating, (r, o) => new
                    {
                        JobName = o.Name, 
                        Factor = r.Factor
                    })
                    .Where(o => o.JobName == customerData.Occupation.ToLower())
                    .Select(o => o.Factor).First();
                // (Death Cover amount * Occupation Rating Factor * Age) /1000 * 12
                double premium = (customerData.DeathBenefit * factor * customerData.Age) / monthlyDenominator;
                return new MonthlyPremium()
                {
                    Premium = premium,
                    Errors = new List<string>()
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
            try
            {
                List<string> occupationNames = _dbContext.Occupations.Select(o => o.Name).ToList();
                return new Occupations()
                {
                    OccupationNames = occupationNames,
                    Errors = new List<string>()
                };
            } catch (Exception e)
            {
                return new Occupations()
                {
                    OccupationNames = new List<string>(),
                    Errors = new List<string>()
                    {
                        "Error reading in occupation names: " + e.Message
                    }
                };
            }

        }
    }
}