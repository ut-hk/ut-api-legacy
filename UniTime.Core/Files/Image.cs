using System;
using UniTime.Files.Enums;
using UniTime.Users;

namespace UniTime.Files
{
    public class Image : File
    {
        public static readonly string[] AllowedExtensions = {".jpg", ".jpeg", ".png"};

        protected Image()
        {
        }

        public override FileType Type { get; } = FileType.Image;

        public static Image Create(string fileName, User owner)
        {
            return new Image
            {
                Id = Guid.NewGuid(),
                OriginalFileName = fileName,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}