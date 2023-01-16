using DNSLab.DTOs.IP;

namespace DNSLab.Interfaces.Repository
{
    public interface IIPRepository
    {
        Task<PingDTO> Ping(string hostOrIPAddress);
        Task<bool?> IsIPAndPortOpen(string hostOrIPAddress, string port);
    }
}
