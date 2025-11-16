using LabPort.Backend.Domain.Common;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string ?PhoneNumber { get; set; }
        public Role Role { get; set; } = Role.User;
        public required string PasswordHash { get; set; }

        public required virtual Container Container { get; set; }

        public DateTime? LastLoginAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}