using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class JobOrderFault
{
    public int JobOrderFaultId { get; set; }

    public int JobOrderId { get; set; }

    public string FaultDescription { get; set; } = null!;

    public string Severity { get; set; } = null!;

    public int RequiredSpecializationId { get; set; }

    public decimal LaborCost { get; set; }

    public int ApprovalStatus { get; set; }

    public DateTime? DecisionDate { get; set; }

    public string FaultStatus { get; set; } = null!;

    public virtual JobOrder JobOrder { get; set; } = null!;

    public virtual ICollection<JobOrderFaultAssignment> JobOrderFaultAssignments { get; set; } = new List<JobOrderFaultAssignment>();

    public virtual ICollection<JobOrderFaultSparePart> JobOrderFaultSpareParts { get; set; } = new List<JobOrderFaultSparePart>();

    public virtual Specialization RequiredSpecialization { get; set; } = null!;
}
