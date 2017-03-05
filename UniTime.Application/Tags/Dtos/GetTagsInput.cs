using System.ComponentModel.DataAnnotations;

namespace UniTime.Tags.Dtos
{
    public class GetTagsInput
    {
        [Required]
        [StringLength(Tag.MaxTextLength, MinimumLength = Tag.MinTextLength)]
        public string QueryText { get; set; }
    }
}