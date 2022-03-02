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
            /*
                .ForMember(d => d.Name, t => t.MapFrom(s => s.Name))
                .ForMember(d => d.Age, t => t.MapFrom(s => s.Age))
                .ForMember(d => d.DeathBenefit, t => t.MapFrom(s => s.DeathBenefit))
                .ForMember(d => d.DateOfBirth, t => t.MapFrom(s => s.DateOfBirth))
                .ForMember(d => d.Occupation, t => t.MapFrom(s => s.Occupation));
            */

            CreateMap<Code_Test.Domain.Models.MonthlyPremium, Code_Test_API.Models.MonthlyPremiumResponse>();

            CreateMap<Code_Test.Domain.Models.Occupations, Code_Test_API.Models.GetOccupationsResponse>()
                .ForMember(d => d.Occupations, t => t.MapFrom(s => s.OccupationNames));
        }
    }
}