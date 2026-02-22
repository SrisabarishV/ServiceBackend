using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.Enum
{
    public enum IntentStatus : long
    {
        PendingApproval = 10,
        Approved = 11,
        Converted = 12,
        Rejected = 13
    }

    public enum PodStatus : long
    {
        Pending = 20,
        Uploaded = 21
    }

    public enum TripStatus : long
    {
        Pending = 30,
        Assigned = 31,
        InTransit = 32,
        Delivered = 33
    }

    public enum TruckStatus : long
    {
        Available = 40,
        OnTrip = 41,
        Maintenance = 42
    }

    public enum DriverStatus : long
    {
        Available = 50,
        OnTrip = 51,
        OnLeave = 52
    }
}
