using System.ComponentModel.DataAnnotations;

namespace hospitalapp.Models.AddModel
{
    public class AddPatientModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; } // Should match the format in your database (e.g., "M", "F", "O")

        [Required]
        public string ContactInfo { get; set; }

        public string EmerContactInfo { get; set; } // Nullable

        [Required]
        public string Address { get; set; }

        public string CurrentMedication { get; set; } // Nullable
    }
}
