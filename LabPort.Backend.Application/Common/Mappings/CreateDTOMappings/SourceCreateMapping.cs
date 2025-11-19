using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class SourceCreateMapping : AutoMapper.Profile
    {
        public SourceCreateMapping() 
        {
            CreateMap<SourceCreateDto, Source>();
        }
    }
}