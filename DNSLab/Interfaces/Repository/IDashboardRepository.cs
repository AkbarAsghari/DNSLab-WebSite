using DNSLab.DTOs.Dashboard;

namespace DNSLab.Interfaces.Repository
{
    public interface IDashboardRepository
    {
        Task<DashboardDTO> GetStats();
    }
}
