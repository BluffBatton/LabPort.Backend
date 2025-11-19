using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class TestGetMapping : AutoMapper.Profile
    {
        public TestGetMapping()
        {
            CreateMap<Test, TestDto>()
                .ForMember(d => d.SampleName,
                    opt => opt.MapFrom(s => s.Sample.Name))
                .ForMember(d => d.TestTypeName,
                    opt => opt.MapFrom(s => s.TestType!.Name));
        }
    }
}
