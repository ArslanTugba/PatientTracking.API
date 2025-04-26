using PatientTracking.API.Models;

namespace PatientTracking.API.Services
{
    public interface IPatientService
    {
        Patient GetPatientById(int id);
        List<Patient> GetAllPatients();
        void AddPatient(Patient patient);
    }
}
