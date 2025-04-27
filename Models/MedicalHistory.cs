using System.ComponentModel.DataAnnotations;

namespace PatientTracking.API.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Treatment is required")]
        public string Treatment { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
