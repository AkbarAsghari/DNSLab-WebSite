
using DNSLab.DTOs.DNS;
using DNSLab.Interfaces.Repository;
using DNSLab.Repository;

namespace DNSLab.Shared.Components.DNS.Integration;

partial class Routers
{
    [Inject] IDNSRepository DNSRepository { get; set; }
    IEnumerable<HostSummaryDTO> HostNames;
    HostSummaryDTO SelectedHostName;

    protected override async Task OnInitializedAsync()
    {
        HostNames = await DNSRepository.GetHostSummaries();
        if (HostNames.Count() > 0)
        {
            SelectedHostName = HostNames.First();
        }
    }
}
