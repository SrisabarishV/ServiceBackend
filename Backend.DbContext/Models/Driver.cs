using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Driver
{
    public long Driverid { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Phonenumber { get; set; } = null!;

    public string Licensenumber { get; set; } = null!;

    public long? DriverStatusId { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual Appstatus? DriverStatus { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
