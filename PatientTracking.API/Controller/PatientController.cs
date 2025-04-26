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

        // Constructor Injection
        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        // POST: api/Patient
        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient data is required");
            }

            _patientService.AddPatient(patient);  // İşlem burada yapılır
            return Ok(patient);  // Ekleme başarılıysa hastayı geri döneriz
        }

        // GET: api/Patient/{id}
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

        // GET: api/Patient
        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientService.GetAllPatients();
            return Ok(patients);  
        }

        // DELETE: api/Patient/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            _patientService.DeletePatient(id);
            return NoContent();  
        }
    }
}
