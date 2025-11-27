using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SensorGetMapping : AutoMapper.Profile
    {
        public SensorGetMapping()
        {
            CreateMap<Sensor, SensorDto>();
        }
    }
}
