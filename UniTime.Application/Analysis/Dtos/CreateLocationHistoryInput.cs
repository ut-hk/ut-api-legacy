using System;

namespace UniTime.Analysis.Dtos
{
    public class CreateLocationHistoryInput
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public Guid GuestId { get; set; }
    }
}