using System.ComponentModel.DataAnnotations;

namespace hospitalapp.Models.UpdateModel
{
    public class UpdateDoctorModel
    {
        [Required]
        public int EmployeeId { get; set; } // Required to identify the employee

        [MaxLength(100)]
        public string Name { get; set; }

        public DateOnly? Dob { get; set; }

        [MaxLength(1)] // Gender is typically a single character (e.g., 'M', 'F')
        public string Gender { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Salary { get; set; }

        public DateOnly? StartDate { get; set; }

        [Range(0, int.MaxValue)]
        public int? Experience { get; set; }

        [MaxLength(100)]
        public string Degree { get; set; }

        public int? DepartmentId { get; set; } // Nullable if not provided

        [MaxLength(100)]
        public string Specialty { get; set; } // Doctor-specific field

        [MaxLength(100)]
        public string Certificate { get; set; } // Doctor-specific field
    }

}
