using System;
using System.Collections.Generic;

namespace ClientKursBus2.Models
{

    public partial class Race
    {
        public int RaceId { get; set; }

        public string? RaceNumber { get; set; }

        public int TripId { get; set; }

        public string? RaceData { get; set; }

        public TimeOnly? RaceTime { get; set; }

        public int? Load { get; set; }

        public Decimal? Profit { get; set; }

        public string? Circulation { get; set; }


        public Schedule? Schedule { get; set; }


    }
}
