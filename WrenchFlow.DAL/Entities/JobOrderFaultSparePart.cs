using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class JobOrderFaultSparePart
{
    public int FaultPartId { get; set; }

    public int JobOrderFaultId { get; set; }

    public int SparePartId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public int WarrantyMonths { get; set; }

    public virtual JobOrderFault JobOrderFault { get; set; } = null!;

    public virtual SparePart SparePart { get; set; } = null!;
}
