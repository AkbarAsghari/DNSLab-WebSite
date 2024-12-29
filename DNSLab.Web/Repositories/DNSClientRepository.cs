using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class DNSClientRepository(IHttpServiceProvider _HttpServiceProvider) : IDNSClientRepository
    {
        const string APIController = "DNSClient";

        public async Task<string?> Query(QueryTypeEnum queryType, string query, string? nameServer = null)
        {
            return await _HttpServiceProvider.Get<string?>($"{APIController}/Query?type={(int)queryType}&query={query}{(String.IsNullOrWhiteSpace(nameServer) ? String.Empty : $"&nameServer={nameServer}")}");

        }

        public Task<string> QueryReverse(string ip)
        {
            throw new NotImplementedException();
        }
    }
}
