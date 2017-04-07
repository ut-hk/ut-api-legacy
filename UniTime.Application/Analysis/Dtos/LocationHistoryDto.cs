using Abp.AutoMapper;

namespace UniTime.Analysis.Dtos
{
    [AutoMapFrom(typeof(LocationHistory))]
    public class LocationHistoryDto
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}