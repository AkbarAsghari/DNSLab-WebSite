using DNSLab.Web.DTOs.Repositories.Zone;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IZoneRepository
    {
        Task<bool> CreateZone(ZoneDTO model);
        Task<bool> DeleteZone(Guid Id);
        Task<bool> DisableZone(Guid Id);
        Task<bool> EnableZone(Guid Id);
        Task<ZoneDTO?> GetZone(Guid Id);
        Task<IEnumerable<ZoneDTO>?> GetZones();
        Task<IEnumerable<string>?> GetRequiredToUpdateNameServers(Guid zoneId);
    }
}
