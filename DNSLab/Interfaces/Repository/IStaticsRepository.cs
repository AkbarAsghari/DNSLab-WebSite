namespace DNSLab.Interfaces.Repository
{
    public interface IStaticsRepository
    {
        Task<bool> PageVisit(string ip, string url);
    }
}
