using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Billing
{
    public int BillingId { get; set; }

    public string? TypeBill { get; set; }

    public string? StatusBill { get; set; }

    public decimal? InitialAmount { get; set; }

    public decimal? CoverAmount { get; set; }

    public DateOnly? DateIssued { get; set; }

    public DateOnly? DueDate { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
}
