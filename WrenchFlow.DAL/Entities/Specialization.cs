using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string SpecializationName { get; set; } = null!;

    public virtual ICollection<JobOrderFault> JobOrderFaults { get; set; } = new List<JobOrderFault>();

    public virtual ICollection<UserSpecialization> UserSpecializations { get; set; } = new List<UserSpecialization>();
}
