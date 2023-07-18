using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools.MainTools;
partial class Port
{
    private HostOrIPAddressAndPortDTO hostOrIPAddressAndPort = new HostOrIPAddressAndPortDTO();
    private bool isProgressing = false;
    private List<string> result = new List<string>();

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    [Parameter, SupplyParameterFromQuery]
    public string? host { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!String.IsNullOrEmpty(host) && host.Contains(":"))
        {
            var splitedHost = host.Split(':');
            hostOrIPAddressAndPort.HostOrIPAddress = splitedHost[0];
            hostOrIPAddressAndPort.Port = splitedHost[1];
            await OnValidSubmit();
        }
    }

    public async Task OnValidSubmit()
    {
        if (isProgressing) return;

        isProgressing = true;

        result.Clear();

        if (host != $"{hostOrIPAddressAndPort.HostOrIPAddress}:{hostOrIPAddressAndPort.Port}")
            Navigation.NavigateTo($"tools/port?host={hostOrIPAddressAndPort.HostOrIPAddress}:{hostOrIPAddressAndPort.Port}");

        var response = await iPRepository.IsIPAndPortOpen(hostOrIPAddressAndPort.HostOrIPAddress, hostOrIPAddressAndPort.Port);
        if (response != null)
        {
            if (response == true)
                result.Add($"{hostOrIPAddressAndPort.HostOrIPAddress}:{hostOrIPAddressAndPort.Port} port is <b style='color: green;'>open</b>");
            else
                result.Add($"{hostOrIPAddressAndPort.HostOrIPAddress}:{hostOrIPAddressAndPort.Port} port is <b style='color: red;'>closed</b>");
        }
        else
        {
            result.Clear();
        }

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddressAndPort.HostOrIPAddress))
            hostOrIPAddressAndPort.HostOrIPAddress = IPDTO.IPv4;

        if (String.IsNullOrEmpty(hostOrIPAddressAndPort.Port))
            hostOrIPAddressAndPort.Port = "80";
    }
}
