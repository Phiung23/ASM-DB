using System.ComponentModel.DataAnnotations;

namespace hospitalapp.Models.AddModel
{
    public class AddNurseModel
    {
        public string Name { get; set; }
        public DateOnly Dob { get; set; }
        [MaxLength(1)]
        [RegularExpression("M|F|O", ErrorMessage = "Gender must be M, F, or O.")]
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public DateOnly StartDate { get; set; }
        public int Experience { get; set; }
        public string Degree { get; set; }
        public int? DepartmentId { get; set; }
        public string Specialty { get; set; }
    }
}
