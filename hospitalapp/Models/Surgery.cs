using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Surgery
{
    public int SurgeryId { get; set; }

    public string? TypeSurgery { get; set; }

    public string? Complication { get; set; }

    public string? Outcome { get; set; }

    public DateOnly? DateSurgery { get; set; }

    public virtual ICollection<PerformSurgery> PerformSurgeries { get; set; } = new List<PerformSurgery>();

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
