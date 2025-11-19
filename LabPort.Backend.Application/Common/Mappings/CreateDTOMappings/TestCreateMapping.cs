using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.Enums;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class TestCreateMapping : AutoMapper.Profile
    {
        public TestCreateMapping()
        {
            CreateMap<TestCreateDto, Test>()
                .ForMember(dest => dest.TestedAt,
                           opt => opt.MapFrom(src => src.TestedAt ?? DateTime.UtcNow))
                .ForMember(dest => dest.TestStatus,
                           opt => opt.MapFrom(src => TestStatus.Await));
        }
    }
}
