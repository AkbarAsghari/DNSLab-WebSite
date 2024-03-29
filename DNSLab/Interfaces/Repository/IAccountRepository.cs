﻿using DNSLab.DTOs.User;

namespace DNSLab.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<UserInfo> Get();
        Task<SettingsDTO> GetSettings();
        Task<IEnumerable<UserDTO>> GetAll();
        Task<string> Login(AuthenticateDTO userInfo);
        Task<bool> ForgetPassword(ForgetPasswordDTO forgetPassword);
        Task<bool> ResetPassword(ResetPasswordDTO resetPassword);
        Task<bool> ChangePassword(ChangePasswordDTO changePassword);
        Task<string> Register(RegisterUserDTO registerUser);
        Task<bool> ChangeEmail(ChangeEmailDTO changeEmail);
        Task<bool> ResendConfirmEmailToken();
        Task<bool> ConfirmEmailWithToken(string Token);
        Task<bool> Update(UserInfo userInfo);
        Task<bool> UpdateSettings(SettingsDTO updateNotifications);
        Task<bool> UpdateUsername(string? Username);
        Task<int> UsersCount();
        Task<bool> DeactivateAccount();
    }
}
