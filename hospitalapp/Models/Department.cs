using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Department
{
    public int DepartmentNumber { get; set; }

    public int? ManageId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee? Manage { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
