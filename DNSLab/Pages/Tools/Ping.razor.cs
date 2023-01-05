using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools; 
partial class Ping 
{
    private HostOrIPAddressDTO hostOrIPAddress = new HostOrIPAddressDTO();
    private bool isProgressing = false;
    private string result = String.Empty;

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    public async Task OnValidSubmit()
    {
        isProgressing = true;

        var response = await iPRepository.IPHavePing(hostOrIPAddress.HostOrIPAddress);
        if (response != null)
        {
            if (response == true)
                result = localizer["PingIsOK"];
            else
                result = localizer["PingIsNotOK"];
        }
        else
        {
            result = String.Empty;
        }

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = IPDTO.IPv4;
    }
}
