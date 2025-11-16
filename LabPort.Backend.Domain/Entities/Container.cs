using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class Container : BaseEntity
    {
        public required string Label { get; set; } = "User Container";
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double HumidityMin { get; set; }
        public double HumidityMax { get; set; }

        public Guid UserId { get; set; }

        public virtual User ?User { get; set; }
        public virtual ICollection<SensorReading> ?SensorReadings { get; set; }
        public virtual ICollection<Sample> ?Samples { get; set; }
    }
}
