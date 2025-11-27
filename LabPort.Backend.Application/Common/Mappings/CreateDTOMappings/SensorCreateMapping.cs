using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SensorCreateMapping : AutoMapper.Profile
    {
        public SensorCreateMapping()
        {
            CreateMap<SensorCreateDto, Sensor>();
        }
    }
}
