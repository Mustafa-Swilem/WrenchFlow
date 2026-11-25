using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int PersonId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<JobOrderFaultAssignment> JobOrderFaultAssignmentAssignedByUsers { get; set; } = new List<JobOrderFaultAssignment>();

    public virtual ICollection<JobOrderFaultAssignment> JobOrderFaultAssignmentAssignedUsers { get; set; } = new List<JobOrderFaultAssignment>();

    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<UserSpecialization> UserSpecializations { get; set; } = new List<UserSpecialization>();

    public virtual ICollection<VehicleRelease> VehicleReleases { get; set; } = new List<VehicleRelease>();
}
