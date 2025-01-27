using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string? Gender { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string JobType { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateOnly StartDate { get; set; }

    public int? Experience { get; set; }

    public string? Degree { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Doctor? Doctor { get; set; }

    public virtual Nurse? Nurse { get; set; }

    public virtual OtherEmployee? OtherEmployee { get; set; }

    public virtual Technician? Technician { get; set; }
}
