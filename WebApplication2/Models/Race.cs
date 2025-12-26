using System;
using System.Collections.Generic;

namespace KursBus2.Models;

public partial class Race
{
    public int RaceId { get; set; } //Айди рейса - 1

    public string? RaceNumber { get; set; }  // Номер рейса - 105-25-0001 - 105-25-7285
    
    public int? TripId { get; set; } // Номер маршрута - 105, из таблицы Schedule

    public string? RaceData { get; set; } // дата  рейса - 12.31.2025

    public TimeOnly? RaceTime { get; set; } // Время рейса - 12.00

    public int? Load { get; set; } //Купленно билетов - 200

    public Decimal? Profit { get; set; } // Денежный оборот рейса - 9000

    public string? Circulation { get; set; } // Загруженность рейса - высокая, низкая

    public virtual Schedule? Schedule { get; set; }
}


