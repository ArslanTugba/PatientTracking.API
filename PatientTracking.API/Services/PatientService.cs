using PatientTracking.API.Models;
using PatientTracking.API.Data;
using Microsoft.EntityFrameworkCore;

namespace PatientTracking.API.Services
{
    public class PatientService
    {
        private readonly AppDbContext _context;

      
        public PatientService(AppDbContext context)
        {
            _context = context;
        }

       
        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();  
        }

     
        public Patient GetPatient(int id)
        {
            return _context.Patients
                           .FirstOrDefault(p => p.Id == id);  
        }

        // Tüm hastaları getirir
        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();  
        }

        // Hasta silme işlemi
        public void DeletePatient(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                _context.Patients.Remove(patient); 
                _context.SaveChanges();  
            }
        }
    }
}
