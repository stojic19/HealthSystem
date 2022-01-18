using Microsoft.AspNetCore.Identity;

namespace HospitalIntegrationTests.DTOs
{
    public class TempUserDTO
    {
        public SignInResult Result { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
