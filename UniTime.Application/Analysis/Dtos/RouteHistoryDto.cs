using Abp.AutoMapper;
using System;

namespace UniTime.Analysis.Dtos
{
    [AutoMapFrom(typeof(RouteHistory))]
    public class RouteHistoryDto
    {
        public string RouteName { get; set; }

        public string Parameters { get; set; }

        public Guid GuestId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}