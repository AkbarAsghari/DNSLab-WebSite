using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.DynamicDNSs;

partial class RouterSettings
{
    [Inject] public IDDNSRepository _DDNSRepository {  get; set; }

    IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>? _AllRecords { get; set; }
    string? _SelectedHostName { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        _AllRecords = await _DDNSRepository.GetDDNSDomainAndRecords();
    }

}
