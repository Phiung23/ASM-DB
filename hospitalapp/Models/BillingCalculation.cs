using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class BillingCalculation
{
    public decimal InitialAmount { get; set; }

    public decimal CoverAmount { get; set; }

    public decimal FinalAmount { get; set; }
}
