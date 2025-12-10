using System;
using System.Collections.Generic;

namespace ClientKursBus2.Models;

public partial class Schedule
{
    public TimeOnly? TimeDepar { get; set; }

    public TimeOnly? TimeArrival { get; set; }

    public string? Name { get; set; }

    public int? Route { get; set; }

    public int? Price { get; set; }

    public int TripId { get; set; }

    public int BusId { get; set; }
}
