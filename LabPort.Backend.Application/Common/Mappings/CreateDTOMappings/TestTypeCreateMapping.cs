using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class TestTypeCreateMapping : AutoMapper.Profile
    {
        public TestTypeCreateMapping() 
        {
            CreateMap<TestTypeCreateDto, TestType>();
        }
    }
}
