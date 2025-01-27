using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Nurse
{
    public int NurseId { get; set; }

    public string? Specialty { get; set; }

    public virtual ICollection<AssignNurse> AssignNurses { get; set; } = new List<AssignNurse>();

    public virtual Employee NurseNavigation { get; set; } = null!;

    public virtual ICollection<PerformTest> PerformTests { get; set; } = new List<PerformTest>();
}
