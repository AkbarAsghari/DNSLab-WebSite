using DNSLab.DTOs.Tips;
using DNSLab.DTOs.Transactions;

namespace DNSLab.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task<string> Tip(TipTransactionDTO request);
        Task<bool> Verify(long trackId);
        Task<IEnumerable<TipDTO>> GetTips();
    }
}
