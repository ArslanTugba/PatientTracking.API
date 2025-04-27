using Microsoft.AspNetCore.Mvc;
using PatientTracking.API.Models;
using PatientTracking.API.Services;

namespace PatientTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

      
        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        
        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient data is required");
            }

            _patientService.AddPatient(patient);  
            return Ok(patient);  
        }

        
        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {
            var patient = _patientService.GetPatient(id);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            return Ok(patient);  
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientService.GetAllPatients();
            return Ok(patients);  
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] Patient updatedPatient)
        {
            if (updatedPatient == null || id != updatedPatient.Id)
            {
                return BadRequest("Invalid patient data");
            }

            var existingPatient = _patientService.GetPatient(id);
            if (existingPatient == null)
            {
                return NotFound("Patient not found");
            }

            // Burada güncelleme yapılır
            existingPatient.Name = updatedPatient.Name;
            existingPatient.Surname = updatedPatient.Surname;
            existingPatient.BirthDate = updatedPatient.BirthDate;

            _patientService.UpdatePatient(existingPatient);

            return Ok(existingPatient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            _patientService.DeletePatient(id);
            return NoContent();  
        }
    }
}
