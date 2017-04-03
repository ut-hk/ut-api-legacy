using System.Collections.Generic;

namespace UniTime.AbstractActivities.Dtos
{
    public class GetActivityTemplatesOutput
    {
        public IReadOnlyList<ActivityTemplateListDto> ActivityTemplates { get; set; }
    }
}