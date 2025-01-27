using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Maintain> Maintains { get; set; } = new List<Maintain>();

    public virtual ICollection<Surgery> Surgeries { get; set; } = new List<Surgery>();

    public virtual ICollection<DiagnosticTest> Tests { get; set; } = new List<DiagnosticTest>();
}
