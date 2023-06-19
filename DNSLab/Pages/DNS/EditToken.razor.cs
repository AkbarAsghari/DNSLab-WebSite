using DNSLab.DTOs.DNS;
using static MudBlazor.CategoryTypes;

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
        tokenAndDNS.HostNameIds.Clear();
        HostSummariesAndChecked.Where(x => x.IsChecked == true).ToList().ForEach(x => tokenAndDNS.HostNameIds.Add(x.Id));

        if (await dnsRepository.UpdateTokensDomainNameSystems(tokenAndDNS))
        {
            _navManager.NavigateTo("dns/mytokens");
            Snackbar.Add(localizer["TokenUpdated"], MudBlazor.Severity.Success);
        }
    }
}
