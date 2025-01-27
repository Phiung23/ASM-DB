using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Allergy
{
    public int RecordId { get; set; }

    public string Allergy1 { get; set; } = null!;

    public virtual PatientRecord Record { get; set; } = null!;
}
