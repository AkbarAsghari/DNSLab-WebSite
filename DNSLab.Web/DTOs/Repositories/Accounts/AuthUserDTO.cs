namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class AuthUserDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
