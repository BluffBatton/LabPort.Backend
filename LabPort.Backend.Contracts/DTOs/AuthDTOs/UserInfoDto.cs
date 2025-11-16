using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.AuthDTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
