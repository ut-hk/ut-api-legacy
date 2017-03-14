using System.Collections.Generic;

namespace UniTime.Files.Dtos
{
    public class GetMyImagesOutput
    {
        public IReadOnlyList<FileDto> Images { get; set; }
    }
}