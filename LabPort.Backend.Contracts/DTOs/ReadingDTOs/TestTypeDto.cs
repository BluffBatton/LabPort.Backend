namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class TestTypeDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double ?ReferenceMin { get; set; }
        public double ?ReferenceMax { get; set; }
        public string ?Unit { get; set; }
    }
}
