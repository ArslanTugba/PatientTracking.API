using System.ComponentModel.DataAnnotations;

namespace PatientTracking.API.Models
{
    public class DoctorComment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
