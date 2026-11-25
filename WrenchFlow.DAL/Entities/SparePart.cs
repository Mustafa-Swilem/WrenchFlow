using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class SparePart
{
    public int SparePartId { get; set; }

    public string PartCode { get; set; } = null!;

    public string PartName { get; set; } = null!;

    public int QuantityInStock { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    public int DefaultWarrantyMonths { get; set; }

    public virtual ICollection<JobOrderFaultSparePart> JobOrderFaultSpareParts { get; set; } = new List<JobOrderFaultSparePart>();
}
