using DNSLab.DTOs.DNS;
using MudBlazor;
using Newtonsoft.Json.Linq;

namespace DNSLab.Pages.DNS;
partial class MyTokens
{
    IEnumerable<TokenSummaryDTO> tokenSummaries;

    TokenSummaryDTO deleteRcord { get; set; } = new TokenSummaryDTO();

    private bool isRevoking = false;
    bool showTokenDetails = false;
    bool showDeleteToken = false;

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
                showDeleteToken = false;
                await LoadTokensSummary();
            }
        }
        deleteRcord = new TokenSummaryDTO();
    }
    private TokenDTO token;

    private void ChangeRevokeStatus() => isRevoking = !isRevoking;

    private async Task ContinueRevokeKey()
    {
        var newTokenKey = await dnsRepository.RevokeTokenKey(token.Id);

        if (!String.IsNullOrEmpty(newTokenKey))
        {
            token.Key = newTokenKey;
            isRevoking = false;
        }
    }

    DialogOptions dialogOptions = new DialogOptions
    {
        CloseButton = true,
        DisableBackdropClick = true,
        MaxWidth = MaxWidth.Medium,
    };
    private async Task OpenToken(TokenSummaryDTO record)
    {
        token = await dnsRepository.GetToken(record.Id);
        showTokenDetails = true;
    }
    private async Task DeleteToken(TokenSummaryDTO record)
    {
        deleteRcord = record;
        showDeleteToken = true;
    }

    private void EditToken(TokenSummaryDTO record)
    {
        NavigationManager.NavigateTo("dns/token/edit/" + record.Id);
    }
}
