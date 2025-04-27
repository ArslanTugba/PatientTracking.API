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
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            if (string.IsNullOrWhiteSpace(patient.Name))
                throw new ArgumentException("Patient name cannot be empty", nameof(patient));

            if (string.IsNullOrWhiteSpace(patient.Surname))
                throw new ArgumentException("Patient surname cannot be empty", nameof(patient));

            if (patient.BirthDate > DateTime.Now)
                throw new ArgumentException("Birth date cannot be in the future", nameof(patient));

            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public Patient? GetPatientById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid patient ID", nameof(id));

            return _context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public void UpdatePatient(Patient updatedPatient)
        {
            if (updatedPatient == null)
                throw new ArgumentNullException(nameof(updatedPatient));

            var existingPatient = _context.Patients.FirstOrDefault(p => p.Id == updatedPatient.Id);
            if (existingPatient == null)
                throw new InvalidOperationException($"Patient with ID {updatedPatient.Id} not found");

            _context.Patients.Update(updatedPatient);
            _context.SaveChanges();
        }

        public void DeletePatient(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid patient ID", nameof(id));

            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                throw new InvalidOperationException($"Patient with ID {id} not found");

            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }
    }
}
