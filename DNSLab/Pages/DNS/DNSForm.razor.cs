using DNSLab.DTOs.DNS;
using DNSLab.DTOs.IP;

namespace DNSLab.Pages.DNS;
partial class DNSForm
{
    [Parameter] public HostNameDTO HostName { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public bool IsNewRecord { get; set; } = false;
    [CascadingParameter] public IPDTO IPDTO { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(HostName.IPv4Address))
            HostName.IPv4Address = IPDTO.IPv4;
    }
}
