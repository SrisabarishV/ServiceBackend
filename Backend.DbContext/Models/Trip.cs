using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Trip
{
    public long Tripid { get; set; }

    public string TripNumber { get; set; } = null!;

    public long Intentid { get; set; }

    public long? Truckid { get; set; }

    public long? Driverid { get; set; }

    public bool IsExternalHire { get; set; }

    public string? ExternalTransporterName { get; set; }

    public string? ExternalTruckReg { get; set; }

    public string? ExternalTruckType { get; set; }

    public string? ExternalDriverName { get; set; }

    public string? ExternalDriverPhone { get; set; }

    public long? TripStatusId { get; set; }

    public int? ProgressPercentage { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual BookingIntent Intent { get; set; } = null!;

    public virtual Appstatus? TripStatus { get; set; }

    public virtual Truck? Truck { get; set; }
}
