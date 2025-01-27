using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int? Dno { get; set; }

    public string? TypeRoom { get; set; }

    public string? StatusRoom { get; set; }

    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();

    public virtual Department? DnoNavigation { get; set; }
}
