namespace LabPort.Backend.Contracts.DTOs.Settings
{
    public class SettingsBackupDto
    {
        public DateTime? ExportedAt { get; set; }
        public List<SettingsBackupSourceTypeDto> SourceTypes { get; set; } = [];
        public List<SettingsBackupTestTypeDto> TestTypes { get; set; } = [];
    }

    public class SettingsBackupSourceTypeDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class SettingsBackupTestTypeDto
    {
        public string Name { get; set; } = string.Empty;
        public double? ReferenceMin { get; set; }
        public double? ReferenceMax { get; set; }
        public string? Unit { get; set; }
    }
}
