using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class WalletRepository(IHttpServiceProvider _HttpServiceProvider) : IWalletRepository
    {
        const string APIController = "Wallet";

        public async Task<WalletDTO?> GetWallet()
        {
            return await _HttpServiceProvider.Get<WalletDTO?>($"{APIController}/GetWallet");
        }

        public async Task<IEnumerable<WalletTransactionDTO>?> GetWalletTransactions()
        {
            return await _HttpServiceProvider.Get<IEnumerable<WalletTransactionDTO>?>($"{APIController}/GetWalletTransactions");
        }
    }
}
