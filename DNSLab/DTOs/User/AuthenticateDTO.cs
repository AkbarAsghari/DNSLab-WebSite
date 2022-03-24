using System.ComponentModel.DataAnnotations;

namespace DNSLab.DTOs.User
{
    public class AuthenticateDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
