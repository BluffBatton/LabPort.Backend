using LabPort.Backend.Domain.Common;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Domain.Entities
{
    public class SensorReading : BaseEntity
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public LidPosition LidPosition { get; set; }

        public Guid SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}