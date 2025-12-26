using System;
using System.Collections.Generic;

namespace ClientKursBus2.Models
{

    public partial class Schedule
    {
        public int TripId { get; set; }

        public int TripNum { get; set; }

        public string? ScheduleTime { get; set; }

        public int? PassTraf { get; set; }

        public string? PeakLoad { get; set; }

        public decimal? Price { get; set; }


    }
}
