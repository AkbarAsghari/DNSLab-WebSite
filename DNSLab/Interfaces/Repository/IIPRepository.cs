using DNSLab.DTOs.IP;

namespace DNSLab.Interfaces.Repository
{
    public interface IIPRepository
    {
        Task<IPDTO> GetIP();
    }
}
