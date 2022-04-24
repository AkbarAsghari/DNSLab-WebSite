using DNSLab.DTOs.User;

namespace DNSLab.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<UserInfo> Get();
        Task<string> Login(AuthenticateDTO userInfo);
        Task<bool> ForgetPassword(ForgetPasswordDTO  forgetPassword);
        Task<bool> ResetPassword(ResetPasswordDTO resetPassword);
        Task<bool> ChangePassword(ChangePasswordDTO changePassword);
        Task<string> Register(UserInfo userInfo);
        Task<bool> Update(UserInfo userInfo);
        Task<int> UsersCount();
    }
}
