using System;
using System.Collections.Generic;

namespace ClientKursBus2.Models
{

    public partial class Race
    {
        public int RaceId { get; set; }

        public int? Load { get; set; }

        public string? Pass { get; set; }

        public int? Profit { get; set; }

        public int? Circulation { get; set; }

        public int TripId { get; set; }
    }
}
