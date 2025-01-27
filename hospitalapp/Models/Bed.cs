using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Bed
{
    public int BedNumber { get; set; }

    public int RoomId { get; set; }

    public virtual AssignBed? AssignBed { get; set; }

    public virtual Room Room { get; set; } = null!;
}
