using System;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace UniTime.Files.Dtos
{
    [AutoMapFrom(typeof(File))]
    public class FileDto : IHasCreationTime
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
    }
}