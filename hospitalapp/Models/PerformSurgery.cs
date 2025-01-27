using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class PerformSurgery
{
    public int RecordId { get; set; }

    public int SurgeryId { get; set; }

    public int DoctorId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual PatientRecord Record { get; set; } = null!;

    public virtual Surgery Surgery { get; set; } = null!;
}
