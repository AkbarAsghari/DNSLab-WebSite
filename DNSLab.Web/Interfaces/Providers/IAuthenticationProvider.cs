namespace DNSLab.Web.Interfaces.Providers
{
    public interface IAuthenticationProvider
    {
        Task Login(AuthUserDTO model);
        Task Logout();
    }
}
