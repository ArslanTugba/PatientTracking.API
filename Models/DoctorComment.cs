namespace PatientTracking.API.Models
{
    public class DoctorComment
    {
        public int Id { get; set; } 
        public int PatientId { get; set; } 
        public string Comment { get; set; } 
        public DateTime CreatedDate { get; set; } 
    }
}
