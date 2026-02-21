using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class BookingIntent
{
    public long Intentid { get; set; }

    public string IntentNumber { get; set; } = null!;

    public long Clientid { get; set; }

    public decimal IntentAmount { get; set; }

    public string? TruckType { get; set; }

    public string? OperationMode { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string DeliveryLocation { get; set; } = null!;

    public string? Material { get; set; }

    public decimal? WeightMt { get; set; }

    public string? OperationalComments { get; set; }

    public long? IntStatusId { get; set; }

    public long? PodStatusId { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Appstatus? IntStatus { get; set; }

    public virtual Appstatus? PodStatus { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
