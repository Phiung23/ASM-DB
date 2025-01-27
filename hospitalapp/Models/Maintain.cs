using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Maintain
{
    public int TechId { get; set; }

    public int EquipId { get; set; }

    public string TypeMaintain { get; set; } = null!;

    public DateOnly DateMaintain { get; set; }

    public virtual Equipment Equip { get; set; } = null!;

    public virtual Technician Tech { get; set; } = null!;
}
