namespace PatientTracking.API.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; } 
        public int PatientId { get; set; } 
        public string Diagnosis { get; set; } 
        public string Treatment { get; set; } 
        public DateTime VisitDate { get; set; } 
    }
}
