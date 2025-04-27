namespace PatientTracking.API.DTO
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserResponseDto User { get; set; }
    }
} 