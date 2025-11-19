using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SampleCreateMapping : AutoMapper.Profile
    {
        public SampleCreateMapping() 
        {
            CreateMap<SampleCreateDto, Sample>();
        }
    }
}
