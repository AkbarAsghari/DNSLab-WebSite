using DNSLab.Repository;

namespace DNSLab.Interfaces.Repository
{
    public interface IRealTimeCommunicationRepository
    {
        Task CheckOnlineUsersCount(string ipAddress);
        event UpdateUsersCountEventHandler OnUpdateUsersCount;
    }
}
