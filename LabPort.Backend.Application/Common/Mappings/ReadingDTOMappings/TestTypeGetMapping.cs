using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class TestTypeGetMapping : AutoMapper.Profile
    {
        public TestTypeGetMapping()
        {
            CreateMap<TestType, TestTypeDto>();
        }
    }
}
