using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class CreateToken
{
    private TokenAndDNSDTO tokenAndDNS = new TokenAndDNSDTO()
    {
        HostNameIds = new List<Guid>()
    };

    private List<HostSummaryAndCheckedDTO> HostSummariesAndChecked;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var HostSummaries = await dnsRepository.GetHostSummaries();

            HostSummariesAndChecked = new List<HostSummaryAndCheckedDTO>();

            foreach (var item in HostSummaries)
                HostSummariesAndChecked.Add(new HostSummaryAndCheckedDTO
                {
                    Id = item.Id,
                    Address = item.Address,
                    IsChecked = tokenAndDNS.HostNameIds.Contains(item.Id)
                });

            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    public async Task Create()
    {
        if (await dnsRepository.GenerateTokenForAccessToUpdateHostNameSystem(tokenAndDNS))
        {
            _navManager.NavigateTo("dns/mytokens");
            toastService.ShowToast(localizer["TokenCreated"], Enums.ToastLevel.Success);
        }
    }
}
