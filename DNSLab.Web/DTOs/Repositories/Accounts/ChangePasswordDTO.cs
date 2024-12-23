using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "رمز عبور جاری را وارد نمایید")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "رمز عبور جدید را وارد نمایید")]
        public string NewPassword { get; set; }
    }
}
