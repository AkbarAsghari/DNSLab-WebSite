using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class WalletRepository(IHttpServiceProvider _HttpServiceProvider) : IWalletRepository
    {
        const string APIController = "Wallet";

        public async Task<IEnumerable<Tuple<DateTime, int>>?> GetLast30DaysTransactionsChartData()
        {
            return await _HttpServiceProvider.Get<IEnumerable<Tuple<DateTime, int>>?>($"{APIController}/GetLast30DaysTransactionsChartData");
        }

        public async Task<WalletDTO?> GetWallet()
        {
            return await _HttpServiceProvider.Get<WalletDTO?>($"{APIController}/GetWallet");
        }

        public async Task<IEnumerable<WalletTransactionDTO>?> GetWalletTransactions(int startIndex, int count)
        {
            return await _HttpServiceProvider.Get<IEnumerable<WalletTransactionDTO>?>($"{APIController}/GetWalletTransactions?startIndex={startIndex}&count={count}");
        }

        public async Task<int?> GetWalletTransactionsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetWalletTransactionsCount");
        }
    }
}
