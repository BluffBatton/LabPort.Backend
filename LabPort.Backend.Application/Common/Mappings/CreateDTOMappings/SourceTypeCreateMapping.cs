using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SourceTypeCreateMapping : AutoMapper.Profile
    {
        public SourceTypeCreateMapping() 
        {
            CreateMap<SourceTypeCreateDto, SourceType>();
        }
    }
}
