using UniTime.Files.Enums;

namespace UniTime.Files
{
    public class Image : File
    {
        public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };

        public override FileType Type { get; } = FileType.Image;
    }
}