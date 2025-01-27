namespace hospitalapp.Models.UpdateModel
{
    public class UpdateNurseModel
    {
        public int EmployeeId { get; set; } // Required to identify the nurse
        public string Name { get; set; }
        public DateOnly? Dob { get; set; }
        public string Gender { get; set; } // Ensure it is either "M" or "F"
        public string PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public DateOnly? StartDate { get; set; }
        public int? Experience { get; set; }
        public string Degree { get; set; }
        public int? DepartmentId { get; set; }
        public string Specialty { get; set; } // From NURSE table
    }

}
