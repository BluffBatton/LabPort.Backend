using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SensorReadingCreateMapping : AutoMapper.Profile
    {
        public SensorReadingCreateMapping()
        {
            CreateMap<SensorReadingCreateDto, SensorReading>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.SensorId, opt => opt.Ignore())
                .ForMember(d => d.Sensor, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());
        }
    }
}
