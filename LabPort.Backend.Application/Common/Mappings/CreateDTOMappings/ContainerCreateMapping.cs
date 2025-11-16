using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class ContainerCreateMapping : AutoMapper.Profile
    {
        public ContainerCreateMapping()
        {
            CreateMap<ContainerCreateDto, Container>();
        }
    }
}
