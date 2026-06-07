using LabPort.Backend.Domain.Common;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Domain.Entities
{
    public class Sensor : BaseEntity
    {
        public required string SerialName { get; set; }
        public required string DeviceKey { get; set; }

        public Guid ContainerId { get; set; }
        public required virtual Container Container { get; set; }

        public LidPosition CurrentLidPosition { get; set; } = LidPosition.Closed;
        public virtual ICollection<SensorReading> ?Readings { get; set; }
    }
}