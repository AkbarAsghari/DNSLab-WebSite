using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class EditToken
{
    [Parameter] public Guid TokenId { get; set; }

    private TokenAndDNSDTO tokenAndDNS;
    private List<HostSummaryAndCheckedDTO> HostSummariesAndChecked;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            tokenAndDNS = await dnsRepository.GetToken(TokenId);
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

    public async Task Update()
    {
        if (await dnsRepository.UpdateTokensDomainNameSystems(tokenAndDNS))
        {
            _navManager.NavigateTo("dns/mytokens");
            toastService.ShowToast(localizer["TokenUpdated"], Enums.ToastLevel.Success);
        }
    }
}
