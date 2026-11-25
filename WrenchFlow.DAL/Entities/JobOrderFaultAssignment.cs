using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class JobOrderFaultAssignment
{
    public int AssignmentId { get; set; }

    public int JobOrderFaultId { get; set; }

    public int AssignedUserId { get; set; }

    public int AssignedByUserId { get; set; }

    public string TaskType { get; set; } = null!;

    public DateTime AssignedDate { get; set; }

    public string AssignmentStatus { get; set; } = null!;

    public virtual User AssignedByUser { get; set; } = null!;

    public virtual User AssignedUser { get; set; } = null!;

    public virtual JobOrderFault JobOrderFault { get; set; } = null!;
}
