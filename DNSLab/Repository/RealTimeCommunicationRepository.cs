using DNSLab.Interfaces.Repository;
using Microsoft.AspNetCore.SignalR.Client;

namespace DNSLab.Repository
{
    public delegate void UpdateUsersCountEventHandler(int count);
    public class RealTimeCommunicationRepository : IRealTimeCommunicationRepository
    {
        public event UpdateUsersCountEventHandler OnUpdateUsersCount;
        private HubConnection _HubConnection;

        object _locker = new object();
        public async Task CheckOnlineUsersCount(string ipAddress)
        {
            lock (_locker)
            {
                if (_HubConnection == null)
                {
                    _HubConnection = new HubConnectionBuilder()
                   .WithUrl(new Uri("https://api.dnslab.link/dnslabhub"))
                   .Build();

                    _HubConnection.On<int>("OnlineUsersCountChanged", count => OnUpdateUsersCount?.Invoke(count));

                }
            }

            if (_HubConnection.State == HubConnectionState.Disconnected)
            {
                await _HubConnection.StartAsync();
            }

            await _HubConnection.SendAsync("Online", ipAddress);
        }
    }
}
