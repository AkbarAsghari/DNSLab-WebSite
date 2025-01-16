using DNSLab.Web.DTOs.Repositories.Wallet;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<WalletDTO?> GetWallet();
        Task<int?> GetWalletTransactionsCount();
        Task<IEnumerable<WalletTransactionDTO>?> GetWalletTransactions(int startIndex,int count);
        Task<IEnumerable<Tuple<DateTime, int>>?> GetLast30DaysTransactionsChartData();
    }
}
