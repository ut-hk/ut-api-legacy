using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Abp.Domain.Entities.Auditing;
using UniTime.Files.Enums;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Files
{
    public abstract class File : AuditedEntity<Guid>, IHasOwner
    {
        public static readonly IDictionary<string, string> ContentTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            // Image
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".png", "image/png"}
        };

        [NotMapped]
        public virtual FileType Type { get; }

        public virtual string OriginalFileName { get; set; }

        [NotMapped]
        public virtual string RemoteFileName => Id + Path.GetExtension(OriginalFileName);

        public virtual string Description { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }
    }
}