namespace LabPort.Backend.Contracts.DTOs.Settings
{
    public class SettingsBackupImportResultDto
    {
        public int CreatedSourceTypes { get; set; }
        public int SkippedSourceTypes { get; set; }
        public int CreatedTestTypes { get; set; }
        public int SkippedTestTypes { get; set; }
    }
}
