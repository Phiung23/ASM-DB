using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Insurance
{
    public int InsuranceId { get; set; }

    public string? Provider { get; set; }

    public string? PolicyNumber { get; set; }

    public string? StatusInsurance { get; set; }

    public decimal? CoverPercentage { get; set; }

    public decimal? CoverLimit { get; set; }

    public int? InsurancePriority { get; set; }

    public virtual HaveInsurance? HaveInsurance { get; set; }

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
}
