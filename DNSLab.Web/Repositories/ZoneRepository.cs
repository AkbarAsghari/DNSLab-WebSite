using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class ZoneRepository(IHttpServiceProvider _HttpServiceProvider) : IZoneRepository
    {
        const string APIController = "Zone";
        public async Task<bool> CreateZone(ZoneDTO model)
        {
            return await _HttpServiceProvider.Post<ZoneDTO, bool>($"{APIController}/CreateZone", model);
        }

        public async Task<bool> DeleteZone(Guid Id)
        {
            return await _HttpServiceProvider.Delete<bool>($"{APIController}/DeleteZone?Id={Id}");
        }

        public async Task<bool> DisableZone(Guid Id)
        {
            return await _HttpServiceProvider.Put<bool>($"{APIController}/DisableZone?Id={Id}");
        }

        public Task<bool> EnableZone(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ZoneDTO>?> GetZones()
        {
            return await _HttpServiceProvider.Get<IEnumerable<ZoneDTO>?>($"{APIController}/GetZones");
        }
    }
}
