using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class Source : BaseEntity
    {
        public required string Name { get; set; }
        public string ?Note { get; set; }
        public string ?Location { get; set; }
        public string ?ContactInfo { get; set; }

        public Guid SourceTypeId { get; set; }

        public required SourceType SourceType { get; set; }
        public virtual ICollection<Sample>? Samples { get; set; }
    }
}
