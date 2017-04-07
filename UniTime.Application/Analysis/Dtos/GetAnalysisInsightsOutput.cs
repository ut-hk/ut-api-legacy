using System.Collections.Generic;

namespace UniTime.Analysis.Dtos
{
    public class GetAnalysisInsightsOutput
    {
        public ICollection<RouteHistoryDto> RouteHistories { get; set; }

        public ICollection<LocationHistoryDto> LocationHistories { get; set; }
    }
}