using DNSLab.Helper.Extensions;
using DNSLab.Interfaces.Auth;
using Jdenticon;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace DNSLab.Prividers
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, IAuthService
    {
        private readonly IJSRuntime JS;
        private readonly string TokenKey = "TOKENKEY";
        private AuthenticationState Anonymouse =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        private readonly HttpClient HttpClient;

        public JWTAuthenticationStateProvider(IJSRuntime jS, HttpClient httpClient)
        {
            this.JS = jS;
            this.HttpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await JS.GetFromLocalStorage(TokenKey);
            if (String.IsNullOrEmpty(token))
                return Anonymouse;

            return BuildAuthenticationState(token);
        }

        public AuthenticationState BuildAuthenticationState(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParsClaimsFromJWT(token), "jwt")));
        }

        private IEnumerable<Claim> ParsClaimsFromJWT(string token)
        {
            var claims = new List<Claim>();
            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var paresRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, paresRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.Add(new Claim(ClaimTypes.Name, keyValuePairs["unique_name"].ToString()));
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;

        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            string converted = base64.Replace('-', '+').Replace('_', '/');

            switch (converted.Length % 4)
            {
                case 2: converted += "=="; break;
                case 3: converted += "="; break;
            }

            return Convert.FromBase64String(converted);
        }

        public async Task Login(string token)
        {
            await JS.SetLocalStorage(TokenKey, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await JS.RemoveFromLocalStorage(TokenKey);
            HttpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymouse));
        }
    }
}
