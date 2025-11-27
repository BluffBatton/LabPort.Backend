namespace LabPort.Backend.Contracts.DTOs.Statistics.Admin
{
    public class AdminStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsersLastNDays { get; set; }

        public int TotalSamples { get; set; }
        public int TotalTests { get; set; }

        public int TotalAlerts { get; set; }

        public int DaysWindow { get; set; }
    }
}
