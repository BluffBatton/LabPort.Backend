using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class Sample : BaseEntity
    {
        public required string Name { get; set; }
        public required DateTime CollectedAt { get; set; }

        public Guid ContainerId { get; set; }
        public Guid SourceId { get; set; }

        public required virtual Source Source { get; set; }
        public required virtual Container Container { get; set; }
        public virtual ICollection<Test>? Tests { get; set; }
    }
}
