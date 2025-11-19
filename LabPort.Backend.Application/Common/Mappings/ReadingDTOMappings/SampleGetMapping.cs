using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class SampleGetMapping : AutoMapper.Profile
    {
        public SampleGetMapping() 
        {
            CreateMap<Sample, SampleDto>()
                .ForMember(d => d.ContainerOwnerFullName,
                           o => o.MapFrom(s => s.Container.User.FirstName + " " + s.Container.User.LastName))
                .ForMember(d => d.SourceName,
                           o => o.MapFrom(s => s.Source.Name));
        }
    }
}
