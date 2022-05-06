using DNSLab.DTOs.IP;

namespace DNSLab.Interfaces.Repository
{
    public interface IIPRepository
    {
        Task<bool> IPHavePing(string hostOrIPAddress);
        Task<bool> IsIPAndPortOpen(string hostOrIPAddress, string port);
    }
}
