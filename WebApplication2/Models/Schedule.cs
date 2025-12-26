using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KursBus2.Models;

public partial class Schedule
{
    public int TripId { get; set; } // Ключевое поле, Айди маршрута - 1

    public int TripNum { get; set; } // Номер маршрута - 105

    public string? ScheduleTime { get; set; } // Расписаните маршрута - 12.00 13.00 19.00 20.00

    public int? PassTraf { get; set; } // Пассажиропоток - до 4000 человек в день

    public string? PeakLoad { get; set; } // Заруженность маршрута - высокая 

    public Decimal? Price { get; set; } // 30 рублей

}


