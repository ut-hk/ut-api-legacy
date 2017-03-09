using System;

namespace UniTime.Activities.Dtos
{
    public class GetActivityTemplatesInput
    {
        public string[] TagTexts { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }
}