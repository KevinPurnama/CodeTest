using AutoMapper;
using Code_Test_API.Models;
using Code_Test.Domain.Models;

namespace Code_Test_API
{
    public class PremiumCalculatorMappingProfile : Profile
    {
        public PremiumCalculatorMappingProfile()
        {
            CreateMap<Code_Test_API.Models.MonthlyPremiumRequest, Code_Test.Domain.Models.Customer>();
            CreateMap<Code_Test.Domain.Models.MonthlyPremium, Code_Test_API.Models.MonthlyPremiumResponse>();

            CreateMap<Code_Test.Domain.Models.Occupations, Code_Test_API.Models.GetOccupationsResponse>()
                .ForMember(d => d.Occupations, t => t.MapFrom(s => s.OccupationNames));
        }
    }
}