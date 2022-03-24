namespace DNSLab.Interfaces.Auth
{
    public interface IAuthService
    {
        Task Login(string token);
        Task Logout();
    }
}
