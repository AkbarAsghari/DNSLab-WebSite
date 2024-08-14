using DNSLab.Repository;

namespace DNSLab.Interfaces.Repository
{
    public interface IRealTimeCommunicationRepository
    {
        Task StartListening(string ipAddress);
        event UpdateUsersCountEventHandler OnUpdateUsersCount;
    }
}
