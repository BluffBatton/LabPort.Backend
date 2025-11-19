using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class TestResultGetMapping : AutoMapper.Profile
    {
        public TestResultGetMapping()
        {
            CreateMap<TestResult, TestResultDto>()
                .ForMember(d => d.TestSubject,
                           opt => opt.MapFrom(src => src.Test.Subject));
        }
    }
}
