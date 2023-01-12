using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools;
partial class DNSLookup
{
    private HostOrIPAddressDTO hostOrIPAddress = new HostOrIPAddressDTO();
    private bool isProgressing = false;
    private string result { get; set; }

    public async Task OnValidSubmit()
    {
        isProgressing = true;

        var response = await iPRepository.DNSLookup(hostOrIPAddress.HostOrIPAddress);
        if (!String.IsNullOrEmpty(response))
            result = response;
        else
            result = String.Empty;

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = "dnslab.ir";
    }
}
