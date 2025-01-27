using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class PerformTest
{
    public int RecordId { get; set; }

    public int TestId { get; set; }

    public int NurseId { get; set; }

    public virtual Nurse Nurse { get; set; } = null!;

    public virtual PatientRecord Record { get; set; } = null!;

    public virtual DiagnosticTest Test { get; set; } = null!;
}
