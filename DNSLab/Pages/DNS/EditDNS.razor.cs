using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class EditDNS
{
    private HostNameDTO hostName;

    [Parameter] public Guid HostNameId { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            hostName = await dnsRepository.GetHostName(HostNameId);
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    public async Task Update()
    {
        hostName.Domain = "dnslab.link";

        if (await dnsRepository.UpdateHostName(hostName))
        {
            _navManager.NavigateTo("dns/mydns");
            Snackbar.Add(localizer["HostNameUpdated"], MudBlazor.Severity.Success);
        }
    }
}
