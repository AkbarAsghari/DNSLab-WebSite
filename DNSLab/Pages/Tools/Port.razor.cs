using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools;
partial class Port
{
    private HostOrIPAddressAndPortDTO hostOrIPAddressAndPort = new HostOrIPAddressAndPortDTO();
    private bool isProgressing = false;
    private string result = String.Empty;

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    public async Task OnValidSubmit()
    {
        isProgressing = true;

        var response = await iPRepository.IsIPAndPortOpen(hostOrIPAddressAndPort.HostOrIPAddress, hostOrIPAddressAndPort.Port);
        if (response != null)
        {
            if (response == true)
                result = $"{hostOrIPAddressAndPort.HostOrIPAddress}:{hostOrIPAddressAndPort.Port} port is <b style='color: green;'>open</b>";
            else
                result = $"{hostOrIPAddressAndPort.HostOrIPAddress}:{hostOrIPAddressAndPort.Port} port is <b style='color: red;'>closed</b>";
        }
        else
        {
            result = String.Empty;
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
