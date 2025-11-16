using LabPort.Backend.Domain.Common;

namespace LabPort.Backend.Domain.Entities
{
    public class TestType : BaseEntity
    {
        public required string Name { get; set; }

        public virtual ICollection<Test> ?Tests { get; set; }
    }
}
