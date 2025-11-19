using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class TestResultCreateMapping : AutoMapper.Profile
    {
        public TestResultCreateMapping()
        {
            CreateMap<TestResultCreateDto, TestResult>()
                .ForMember(d => d.PerformedAt,
                           opt => opt.MapFrom(src => src.PerformedAt ?? DateTime.UtcNow));
        }
    }
}
