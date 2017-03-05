using System.Collections.Generic;

namespace UniTime.Tags.Dtos
{
    public class GetTagsOutput
    {
        public IReadOnlyList<TagDto> Tags { get; set; }
    }
}