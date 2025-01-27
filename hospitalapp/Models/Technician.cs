using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Technician
{
    public int EmployeeId { get; set; }

    public string? Specialty { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Maintain> Maintains { get; set; } = new List<Maintain>();
}
