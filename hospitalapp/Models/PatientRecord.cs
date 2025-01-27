using System;
using System.Collections.Generic;

namespace hospitalapp.Models;

public partial class PatientRecord
{
    public int RecordId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;

    public string? EmerContactInfo { get; set; }

    public string? Address { get; set; }

    public string? CurrentMedication { get; set; }

    public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

    public virtual ICollection<AssignBed> AssignBeds { get; set; } = new List<AssignBed>();

    public virtual AssignDoctor? AssignDoctor { get; set; }

    public virtual AssignNurse? AssignNurse { get; set; }

    public virtual ICollection<HaveInsurance> HaveInsurances { get; set; } = new List<HaveInsurance>();

    public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

    public virtual ICollection<PerformSurgery> PerformSurgeries { get; set; } = new List<PerformSurgery>();

    public virtual ICollection<PerformTest> PerformTests { get; set; } = new List<PerformTest>();
}
