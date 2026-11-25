using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class JobOrder
{
    public int JobOrderId { get; set; }

    public int VehicleId { get; set; }

    public int ReceivedByUserId { get; set; }

    public string InitialComplaint { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual ICollection<JobOrderFault> JobOrderFaults { get; set; } = new List<JobOrderFault>();

    public virtual User ReceivedByUser { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;

    public virtual VehicleRelease? VehicleRelease { get; set; }
}
