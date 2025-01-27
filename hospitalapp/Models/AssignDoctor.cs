using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class AssignDoctor
{
    public int? DoctorId { get; set; }

    public int RecordId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual PatientRecord Record { get; set; } = null!;
}
