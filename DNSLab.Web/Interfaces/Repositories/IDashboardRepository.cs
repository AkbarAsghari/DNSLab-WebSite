namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<int?> GetZonesCount();
        Task<int?> GetRecordsCount();
        Task<int?> GetDDNSsCount();
        Task<int?> GetTodayDDNSIpChangesCount();
    }
}
