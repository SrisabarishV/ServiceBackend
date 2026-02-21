using System;
using System.Collections.Generic;

namespace Backend.SQLContext.Models;

public partial class RecentActivity
{
    public long Activityid { get; set; }

    public long Userid { get; set; }

    public string ActionType { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public string? EntityRef { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public virtual User User { get; set; } = null!;
}
