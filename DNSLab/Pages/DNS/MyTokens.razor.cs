using DNSLab.DTOs.DNS;
using Newtonsoft.Json.Linq;

namespace DNSLab.Pages.DNS;
partial class MyTokens
{
    IEnumerable<TokenSummaryDTO> tokenSummaries;

    Modal DeleteModal { get; set; }

    TokenSummaryDTO deleteRcord { get; set; } = new TokenSummaryDTO();

    private bool isRevoking = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadTokensSummary();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task LoadTokensSummary()
    {
        tokenSummaries = await dnsRepository.GetTokenSummary();
    }

    private async Task AcceptDelete()
    {
        if (deleteRcord != null)
        {
            if (await dnsRepository.DeleteToken(deleteRcord.Id))
            {
                await DeleteModal.Close();
                await LoadTokensSummary();
            }
        }
        deleteRcord = new TokenSummaryDTO();
    }

    private async Task OpenToken(TokenSummaryDTO record)
    {
        Dialog.Show<TokenDetails>(title: "جزئیات", parameters: new MudBlazor.DialogParameters
        {
            { "token", await dnsRepository.GetToken(record.Id)  }
        }, new MudBlazor.DialogOptions
        {
            CloseButton = true,
            DisableBackdropClick = true,
            MaxWidth = MudBlazor.MaxWidth.Large,
        });

    }
    private async Task DeleteToken(TokenSummaryDTO record)
    {
        deleteRcord = record;
        await DeleteModal.Open();
    }

    private void EditToken(TokenSummaryDTO record)
    {
        NavigationManager.NavigateTo("dns/token/edit/" + record.Id);
    }
}
