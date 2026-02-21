using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Client
{
    public long Clientid { get; set; }

    public string Companyname { get; set; } = null!;

    public string? Contactperson { get; set; }

    public string? Contactnumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual ICollection<BookingIntent> BookingIntents { get; set; } = new List<BookingIntent>();
}
