using Abp.Domain.Entities.Auditing;

namespace UniTime.Tags
{
    public abstract class Tag : FullAuditedEntity<long>
    {
        public virtual string Text { get; set; }
    }
}