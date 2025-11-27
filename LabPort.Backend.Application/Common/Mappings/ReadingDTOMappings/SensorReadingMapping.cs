using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SensorReadingMapping : AutoMapper.Profile
    {
        public SensorReadingMapping()
        {
            CreateMap<SensorReading, SensorReadingDto>();
        }
    }
}
