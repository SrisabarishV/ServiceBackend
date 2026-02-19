using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class User
{
    public long Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string? Phonenumber { get; set; }

    public string Emailid { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public long? Roleid { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual Role? Role { get; set; }
}
