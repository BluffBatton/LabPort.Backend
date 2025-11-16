namespace LabPort.Backend.Contracts.DTOs.AuthDTOs
{
    public class RefreshTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}