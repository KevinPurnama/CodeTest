using System;
using System.Threading.Tasks;

using Code_Test.Domain.Models;

namespace Code_Test.Domain
{
    public interface IPremiumCalcDomain
    {
        Task<MonthlyPremium> CalcMonthlyPremiumAsync(Customer customerData);

        Task<Occupations> GetOccupationsAsync();
    }
}