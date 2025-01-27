using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class HaveInsurance
{
    public int InsuranceId { get; set; }

    public int? PatientId { get; set; }

    public virtual Insurance Insurance { get; set; } = null!;

    public virtual PatientRecord? Patient { get; set; }
}
