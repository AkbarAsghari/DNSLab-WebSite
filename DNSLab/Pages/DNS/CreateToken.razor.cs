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
                    IsChecked = false
                });

            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    public async Task Create()
    {
        tokenAndDNS.HostNameIds.Clear();
        HostSummariesAndChecked.Where(x => x.IsChecked == true).ToList().ForEach(x => tokenAndDNS.HostNameIds.Add(x.Id));

        if (await dnsRepository.GenerateTokenForAccessToUpdateHostNameSystem(tokenAndDNS))
        {
            _navManager.NavigateTo("dns/mytokens");
            Snackbar.Add(localizer["TokenCreated"], MudBlazor.Severity.Success);
        }
    }
}
