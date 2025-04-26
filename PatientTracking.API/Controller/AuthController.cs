using Microsoft.AspNetCore.Mvc;
using PatientTracking.API.Models;

namespace PatientTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto model)
        {
            // Giriş kontrolü
            if (model.Email == "arslan@amatis.com" && model.Password == "Admin123*")
            {
                return Ok(new
                {
                    token = "fake-jwt-token",
                    user = new
                    {
                        name = "Tuğba Arslan",
                        email = model.Email
                    }
                });
            }

            return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı" });
        }
    }
}
