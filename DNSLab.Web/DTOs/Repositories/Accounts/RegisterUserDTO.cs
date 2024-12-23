using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "وارد کردن رمز عبور ضروری میباشد")]
        public string Email { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        public string Password { get; set; }
        [Required(ErrorMessage = "تکرار رمز عبور را وارد نمایید")]
        public string ConfirmPassword { get; set; }
    }
}
