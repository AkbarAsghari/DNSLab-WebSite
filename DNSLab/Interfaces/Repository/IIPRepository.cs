using DNSLab.DTOs.IP;

namespace DNSLab.Interfaces.Repository
{
    public interface IIPRepository
    {
        Task<bool?> IPHavePing(string hostOrIPAddress);
        Task<bool?> IsIPAndPortOpen(string hostOrIPAddress, string port);
        Task<string> DNSLookup(string hostOrIPAddress);
        Task<string> ReverseLoopUp(string iPAddress);
    }
}
