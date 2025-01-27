using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class AssignNurse
{
    public int? NurseId { get; set; }

    public int RecordId { get; set; }

    public virtual Nurse? Nurse { get; set; }

    public virtual PatientRecord Record { get; set; } = null!;
}
