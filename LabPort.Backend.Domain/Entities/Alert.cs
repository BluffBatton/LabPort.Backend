using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class Alert : BaseEntity
    {
        public required string Message { get; set; }
        public string? Details { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }

        public Guid SensorReadingId { get; set; }
        public required virtual SensorReading SensorReading { get; set; }
    }
}
