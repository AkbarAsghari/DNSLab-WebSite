namespace DNSLab.Web.Interfaces.Providers
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
        Task<string> GetRefreshTokenAsync();
        Task SetTokenAsync(AuthUserDTO model);
        Task DeleteTokenAsync();
    }
}
