using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class InvoiceItem
{
    public int InvoiceItemId { get; set; }

    public int InvoiceId { get; set; }

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
