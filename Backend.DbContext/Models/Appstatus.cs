using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Appstatus
{
    public long Statusid { get; set; }

    public string StatusCategory { get; set; } = null!;

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

    public bool Isactive { get; set; }

    public DateTime? Modifiedat { get; set; }

    public virtual ICollection<BookingIntent> BookingIntentIntStatuses { get; set; } = new List<BookingIntent>();

    public virtual ICollection<BookingIntent> BookingIntentPodStatuses { get; set; } = new List<BookingIntent>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
