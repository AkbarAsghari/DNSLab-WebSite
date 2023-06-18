using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class TokenForm
{
    [Parameter] public string Title { get; set; } = String.Empty;
    [Parameter] public TokenAndDNSDTO TokenAndDNS { get; set; }
    [Parameter] public List<HostSummaryAndCheckedDTO> HostSummariesAndChecked { get; set; }


    [Parameter] public EventCallback OnValidSubmit { get; set; }
}
