using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string? Specialty { get; set; }

    public string? Certifica { get; set; }

    public virtual ICollection<AssignDoctor> AssignDoctors { get; set; } = new List<AssignDoctor>();

    public virtual Employee DoctorNavigation { get; set; } = null!;

    public virtual ICollection<PerformSurgery> PerformSurgeries { get; set; } = new List<PerformSurgery>();
}
