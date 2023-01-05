using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class CreateDNS
{
    private HostNameDTO hostName = new HostNameDTO() { RecordType = 1 };

    public async Task Create()
    {
        hostName.Domain = "dnslab.ir";

        if (await dnsRepository.CreateHostName(hostName))
        {
            _navManager.NavigateTo("dns/mydns");
            toastService.ShowToast(localizer["HostNameCreated"], Enums.ToastLevel.Success);
        }
    }
}
