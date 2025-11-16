using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class UserGetMapping : AutoMapper.Profile
    {
        public UserGetMapping() 
        {
            CreateMap<User, UserDto>();
        }
    }
}
