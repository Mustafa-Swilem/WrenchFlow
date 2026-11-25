using System;
using System.Collections.Generic;

namespace WrenchFlow.DAL.Entities;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int JobOrderId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal Discount { get; set; }

    public decimal TaxRate { get; set; }

    public decimal Tax { get; set; }

    public decimal TotalAmount { get; set; }

    public string InvoiceStatus { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual JobOrder JobOrder { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
