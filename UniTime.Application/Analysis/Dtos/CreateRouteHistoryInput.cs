using System;
using System.ComponentModel.DataAnnotations;

namespace UniTime.Analysis.Dtos
{
    public class CreateRouteHistoryInput
    {
        [Required]
        public string RouteName { get; set; }

        [Required]
        public string Parameters { get; set; }

        public string Referer { get; set; }

        public Guid GuestId { get; set; }
    }
}