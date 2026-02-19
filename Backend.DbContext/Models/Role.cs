using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Role
{
    public long Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public long? Createdby { get; set; }

    public DateTime Modifiedat { get; set; }

    public long? Modifiedby { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
