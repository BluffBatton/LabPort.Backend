using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class Sensor : BaseEntity
    {
        public required string SerialName { get; set; }
        public required string DeviceKey { get; set; }

        public Guid ContainerId { get; set; }
        public required virtual Container Container { get; set; }

        public virtual ICollection<SensorReading> ?Readings { get; set; }
    }
}