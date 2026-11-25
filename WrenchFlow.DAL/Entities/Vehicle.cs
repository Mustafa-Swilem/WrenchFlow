using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int CustomerId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string? Color { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();
}
