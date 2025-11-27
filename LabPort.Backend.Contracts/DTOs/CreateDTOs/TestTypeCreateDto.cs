namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class TestTypeCreateDto
    {
        public required string Name { get; set; }
        public double ?ReferenceMin { get; set; }
        public double ?ReferenceMax { get; set; }
        public string ?Unit { get; set; }
    }
}
