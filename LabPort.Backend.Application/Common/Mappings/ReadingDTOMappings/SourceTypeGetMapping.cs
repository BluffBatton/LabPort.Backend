using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SourceTypeGetMapping : AutoMapper.Profile
    {
        public SourceTypeGetMapping() 
        {
            CreateMap<SourceType, SourceTypeDto>();
        }
    }
}
