using System;
using Abp.AutoMapper;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityDto : AbstractActivity
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}