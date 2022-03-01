using System;
using System.Threading.Tasks;

using Code_Test.Domain.Models;

namespace Code_Test.Domain
{
    public class PremiumCalcDomain : IPremiumCalcDomain
    {
        public Task<MonthlyPremium> CalcMonthlyPremiumAsync(Customer customerData)
        {
            throw new NotImplementedException();
        }

        public Task<Occupations> GetOccupationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}