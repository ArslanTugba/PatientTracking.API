using Microsoft.AspNetCore.Mvc;
using PatientTracking.API.Models;
using PatientTracking.API.Data;

namespace PatientTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Patients.Add(model);
            _context.SaveChanges();

            return Ok(model); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return NotFound(new { message = "Patient not found." });

            return Ok(patient);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var patients = _context.Patients.ToList();
            return Ok(patients);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return Ok(new { message = "Patient has been deleted." });
        }


    }
}
