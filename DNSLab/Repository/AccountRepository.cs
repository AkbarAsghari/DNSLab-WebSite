using DNSLab.DTOs.User;
using DNSLab.Enums;
using DNSLab.Helper.HttpService;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DNSLab.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public AccountRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            _httpService = httpService;
            _memoryCache = memoryCache;
        }

        public async Task<string> Register(RegisterUserDTO registerUser)
        {
            var response = await _httpService.Post<RegisterUserDTO, AuthUserDTO>($"/Auth/Register", registerUser);
            if (!response.Success)
            {
                return String.Empty;
            }
            else
            {
                return response.Response.Token;
            }
        }

        public async Task<bool> Update(UserInfo userInfo)
        {
            var response = await _httpService.Put<UserInfo, bool>($"/Auth/Update", userInfo);
            return response.Response;
        }

        public async Task<string> Login(AuthenticateDTO userInfo)
        {
            var response = await _httpService.Post<AuthenticateDTO, AuthUserDTO>($"/Auth/authenticate", userInfo);
            if (!response.Success)
            {
                return String.Empty;
            }
            else
            {
                return response.Response.Token;
            }
        }

        public async Task<UserInfo> Get()
        {
            var response = await _httpService.Get<UserInfo>($"/Auth/get");
            if (!response.Success)
            {
                return null;
            }
            else
            {
                return response.Response;
            }
        }


        public async Task<int> UsersCount()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.UsersCount, out int cacheValue))
            {
                var result = await _httpService.Get<int>($"/Auth/UsersCount");
                if (!result.Success)
                    cacheValue = 0;
                else
                    cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(CacheKeyEnum.UsersCount, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

        public async Task<bool> ForgetPassword(ForgetPasswordDTO forgetPassword)
        {
            var response = await _httpService.Post<ForgetPasswordDTO, bool>($"/Auth/ForgetPassword", forgetPassword);
            return response.Response;
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var response = await _httpService.Post<ResetPasswordDTO, bool>($"/Auth/ResetPassword", resetPassword);
            return response.Response;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO changePassword)
        {
            var response = await _httpService.Put<ChangePasswordDTO, bool>($"/Auth/ChangePassword", changePassword);
            return response.Response;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var response = await _httpService.Get<IEnumerable<UserDTO>>($"/Auth/getAll");
            if (!response.Success)
            {
                return null;
            }
            else
            {
                return response.Response;
            }
        }

        public async Task<bool> ChangeEmail(ChangeEmailDTO changeEmail)
        {
            var response = await _httpService.Put<ChangeEmailDTO, bool>($"/Auth/ChangeEmail", changeEmail);
            return response.Response;
        }

        public async Task<bool> ResendConfirmEmailToken()
        {
            var response = await _httpService.Post<bool>($"/Auth/ResendConfirmEmailToken");
            return response.Response;
        }

        public async Task<bool> ConfirmEmailWithToken(string Token)
        {
            var response = await _httpService.Post<bool>($"/Auth/ConfirmEmailWithToken?token={Token}");
            return response.Response;
        }

        public async Task<bool> DeactivateAccount()
        {
            var response = await _httpService.Delete<bool>($"/Auth/DeactivateAccount");
            return response.Response;
        }
    }
}
