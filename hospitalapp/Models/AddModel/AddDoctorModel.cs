namespace hospitalapp.Models.AddModel
{
    public class AddDoctorModel
    {
        public string Name { get; set; }
        public DateOnly Dob { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public DateOnly StartDate { get; set; }
        public int Experience { get; set; }
        public string Degree { get; set; }
        public int? DepartmentId { get; set; } // Nullable
        public string Specialty { get; set; }
        public string Certificate { get; set; }
    }
}
