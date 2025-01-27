using System.ComponentModel.DataAnnotations;

namespace hospitalapp.Models
{
    public class UpdatePatientModel
    {
        [Required]
        public int RecordId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string ContactInfo { get; set; }

        public string EmerContactInfo { get; set; }

        public string Address { get; set; }

        public string CurrentMedication { get; set; }
    }
}
