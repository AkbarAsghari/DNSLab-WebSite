using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class ForgetPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
