using DNSLab.DTOs.Tips;

namespace DNSLab.Interfaces.Repository
{
    public interface ITipRepository
    {
        Task<IEnumerable<TipDTO>> GetTips();
    }
}
