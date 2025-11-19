using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SourceGetMapping : AutoMapper.Profile
    {
        public SourceGetMapping() 
        {
            CreateMap<Source, SourceDto>()
                .ForMember(dest => dest.SourceTypeName,
                            opt => opt.MapFrom(src => src.SourceType.Name));
        }
    }
}
