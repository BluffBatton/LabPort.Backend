using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SensorReadingCreateMapping : AutoMapper.Profile
    {
        public SensorReadingCreateMapping()
        {
            CreateMap<SensorReadingCreateDto, SensorReading>();
        }
    }
}
