using LabPort.Backend.Contracts.DTOs.CreateDTOs;

namespace LabPort.Backend.Contracts.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public UserCreateDto User { get; set; }
        public ContainerCreateDto Container { get; set; }
    }
}