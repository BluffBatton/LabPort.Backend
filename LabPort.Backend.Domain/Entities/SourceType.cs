using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class SourceType : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<Source>? Sources { get; set; }
    }
}