using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class Appsetting
{
    public long Settingid { get; set; }

    public string Settingkey { get; set; } = null!;

    public string Settingvalue { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Modifiedat { get; set; }

    public long? Modifiedby { get; set; }
}
