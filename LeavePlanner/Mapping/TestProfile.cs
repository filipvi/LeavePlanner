using AutoMapper;
using LeavePlanner.Core.Models.Test;
using LeavePlanner.Models.DTOs;

namespace LeavePlanner.Mapping
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<TestTable, TestDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.CultureName, opt => opt.MapFrom(s => s.CultureName))
                .ForMember(d => d.CultureCategory, opt => opt.MapFrom(s => s.CultureCategory))
                .ForMember(d => d.SamplesMultiplier, opt => opt.MapFrom(s => s.SamplesMultiplier));
        }
    }
}
