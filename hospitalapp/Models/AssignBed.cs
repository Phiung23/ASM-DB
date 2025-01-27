using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class AssignBed
{
    public int PatientId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int BedNumber { get; set; }

    public int RoomId { get; set; }

    public virtual Bed Bed { get; set; } = null!;

    public virtual PatientRecord Patient { get; set; } = null!;
}
