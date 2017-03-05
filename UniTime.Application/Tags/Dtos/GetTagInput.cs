using System.ComponentModel.DataAnnotations;

namespace UniTime.Tags.Dtos
{
    public class GetTagInput
    {
        [Required]
        [StringLength(Tag.MaxTextLength, MinimumLength = Tag.MinTextLength)]
        public string Text { get; set; }
    }
}