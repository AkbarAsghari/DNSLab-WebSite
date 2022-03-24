using DNSLab.DTOs.User;
using DNSLab.Helper.HttpService;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using System.Text.Json;

namespace DNSLab.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpService _httpService;
        private readonly string baseUrl = "localhost/auth";
        public AccountRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> Register(UserInfo userInfo)
        {
            var response = await _httpService.Post<UserInfo, AuthUserDTO>($"/Auth/Register", userInfo);
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
            var response = await _httpService.Get<int>($"/Auth/UsersCount");
            if (!response.Success)
                return 0;
            else
                return response.Response;
        }
    }
}
