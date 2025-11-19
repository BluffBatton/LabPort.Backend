using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class ContainerGetMapping : AutoMapper.Profile
    {
        public ContainerGetMapping()
        {
            CreateMap<Container, ContainerDto>();
        }
    }
}
