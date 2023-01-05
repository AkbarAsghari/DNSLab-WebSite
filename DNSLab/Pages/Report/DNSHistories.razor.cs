using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.Report;
partial class DNSHistories
{
    List<DNSChangeHistoryDTO> dNSChangeHistories;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            dNSChangeHistories = (await dnsRepository.GetDNSChangeHistories()).ToList();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }
}
