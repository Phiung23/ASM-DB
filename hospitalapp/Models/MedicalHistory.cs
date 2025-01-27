using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class MedicalHistory
{
    public int RecordId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Treatment { get; set; }

    public string? DescriptionDetail { get; set; }

    public string? Stage { get; set; }

    public virtual PatientRecord Record { get; set; } = null!;
}
