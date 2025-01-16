using DNSLab.Web.DTOs.Repositories.Wallet;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<WalletDTO?> GetWallet();
        Task<IEnumerable<WalletTransactionDTO>?> GetWalletTransactions();
        Task<IEnumerable<Tuple<DateTime, int>>?> GetLast30DaysTransactionsChartData();
    }
}
