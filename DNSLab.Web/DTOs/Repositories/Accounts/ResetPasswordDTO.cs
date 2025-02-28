using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class ResetPasswordDTO
    {
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
