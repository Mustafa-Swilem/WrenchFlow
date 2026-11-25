using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class UserSpecialization
{
    public int UserId { get; set; }

    public int SpecializationId { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
