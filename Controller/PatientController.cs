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
            try
            {
                if (patient == null)
                {
                    return BadRequest("Patient data is required");
                }

                _patientService.AddPatient(patient);
                return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {
            try
            {
                var patient = _patientService.GetPatientById(id);
                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} not found");
                }

                return Ok(patient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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

            var existingPatient = _patientService.GetPatientById(id);
            if (existingPatient == null)
            {
                return NotFound("Patient not found");
            }

            existingPatient.Name = updatedPatient.Name;
            existingPatient.Surname = updatedPatient.Surname;
            existingPatient.BirthDate = updatedPatient.BirthDate;
            existingPatient.Comment = updatedPatient.Comment;

            _patientService.UpdatePatient(existingPatient);

            return Ok(existingPatient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                _patientService.DeletePatient(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
