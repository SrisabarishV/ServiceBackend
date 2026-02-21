using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Truck
{
    public long Truckid { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public string TruckType { get; set; } = null!;

    public decimal? CapacityMt { get; set; }

    public long? TruckStatusId { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    public virtual Appstatus? TruckStatus { get; set; }
}
