using DNSLab.DTOs.Tips;
using DNSLab.DTOs.Transactions;

namespace DNSLab.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task<bool> Verify(long trackId);
    }
}
