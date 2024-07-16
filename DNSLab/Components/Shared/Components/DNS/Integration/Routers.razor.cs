
using DNSLab.DTOs.DNS;
using DNSLab.Interfaces.Repository;
using DNSLab.Repository;

namespace DNSLab.Components.Shared.Components.DNS.Integration;

partial class Routers
{
    [Inject] IDNSRepository DNSRepository { get; set; }
    IEnumerable<HostSummaryDTO> HostNames;
    HostSummaryDTO SelectedHostName;

    protected override async Task OnInitializedAsync()
    {
        HostNames = await DNSRepository.GetHostSummaries();
    }
}
