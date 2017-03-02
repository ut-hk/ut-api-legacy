using System.Collections.Generic;

namespace UniTime.Activities.Dtos
{
    public class GetActivityTemplatesOutput
    {
        public IReadOnlyList<ActivityTemplateDto> ActivityTemplates { get; set; }
    }
}