using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class DiagnosticTest
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public string? TestDescription { get; set; }

    public DateOnly TestDate { get; set; }

    public string? TestResult { get; set; }

    public virtual ICollection<PerformTest> PerformTests { get; set; } = new List<PerformTest>();

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
