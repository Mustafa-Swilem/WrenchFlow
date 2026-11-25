using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class VehicleRelease
{
    public int ReleaseId { get; set; }

    public int JobOrderId { get; set; }

    public int ReleasedByUserId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string ReceivedByName { get; set; } = null!;

    public bool HasAdminApproval { get; set; }

    public string? AdminApprovalReason { get; set; }

    public virtual JobOrder JobOrder { get; set; } = null!;

    public virtual User ReleasedByUser { get; set; } = null!;
}
