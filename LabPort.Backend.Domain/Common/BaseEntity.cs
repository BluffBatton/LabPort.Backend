namespace LabPort.Backend.Domain.Common
{
    public class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
