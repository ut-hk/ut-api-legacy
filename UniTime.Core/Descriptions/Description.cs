using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public abstract class Description : Entity<long>
    {
        [NotMapped]
        public virtual string Content { get; set; }

        public virtual DescriptionType Type { get; set; }

        public virtual int Priority { get; set; }
    }
}