namespace LabPort.Backend.Contracts.DTOs.UpdateDTOs
{
    public class TestTypeUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public double? ReferenceMin { get; set; }
        public double? ReferenceMax { get; set; }
        public string? Unit { get; set; }
    }
}
