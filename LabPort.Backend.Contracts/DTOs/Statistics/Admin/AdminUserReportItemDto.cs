namespace LabPort.Backend.Contracts.DTOs.Statistics.Admin
{
    public class AdminUserReportItemDto
    {
        public Guid UserId { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }

        public int SamplesCount { get; set; }
        public int TestsCount { get; set; }
        public int AlertsCount { get; set; }
    }
}
