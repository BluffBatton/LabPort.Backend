using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class AlertReadMapping : AutoMapper.Profile
    {
        public AlertReadMapping()
        {
            CreateMap<Alert, AlertDto>();
        }
    }
}
