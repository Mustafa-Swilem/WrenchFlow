using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? LogDetails { get; set; }

    public DateTime LogDate { get; set; }

    public virtual User? User { get; set; }
}
