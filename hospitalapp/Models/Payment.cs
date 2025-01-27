using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Payment
{
    public int ReceiptId { get; set; }

    public decimal? Amount { get; set; }

    public string? Note { get; set; }

    public string? Method { get; set; }

    public DateOnly? DatePay { get; set; }

    public int? BillingId { get; set; }

    public virtual Billing? Billing { get; set; }
}
