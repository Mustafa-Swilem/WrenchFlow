using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int PersonId { get; set; }

    public int? CreatedByUserId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
