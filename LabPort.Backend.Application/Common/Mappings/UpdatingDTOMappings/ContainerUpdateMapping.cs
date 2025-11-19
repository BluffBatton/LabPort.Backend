using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class ContainerUpdateMapping : AutoMapper.Profile
    {
        public ContainerUpdateMapping()
        {
            CreateMap<ContainerUpdateDto, Container>();
        }
    }
}