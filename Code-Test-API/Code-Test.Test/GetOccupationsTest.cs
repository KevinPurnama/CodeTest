using Microsoft.EntityFrameworkCore;
using Xunit;

using Code_Test.Domain;
using Code_Test.Domain.Models;

namespace Code_Test.Test
{
    public class GetOccupationsTest
    {
        private PremiumCalcDomain _service;
        private FactorDBContext _factorDb;

        public GetOccupationsTest()
        {
            DbContextOptionsBuilder<FactorDBContext> opt = new DbContextOptionsBuilder<FactorDBContext>();
            opt.UseInMemoryDatabase("FactorDBGetOccupationTest");
            _factorDb = new FactorDBContext(opt.Options);
            _service = new PremiumCalcDomain(_factorDb);
        }

        [Fact]
        public void TestGet_OccupationNames()
        {
            Occupations result = _service.GetOccupationsAsync().Result;
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.Equal(6, result.OccupationNames.Count);
            Assert.True(result.OccupationNames.Contains("florist"));
            Assert.True(result.OccupationNames.Contains("doctor"));
            Assert.True(result.OccupationNames.Contains("author"));
        }
    }
}
