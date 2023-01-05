using DNSLab.DTOs.DNS;

namespace DNSLab.Pages.DNS;
partial class MyTokens
{
    IEnumerable<TokenSummaryDTO> tokenSummaries;

    Modal DeleteModal { get; set; }
    Modal TokenDetailsModal { get; set; }

    TokenSummaryDTO deleteRcord { get; set; } = new TokenSummaryDTO();
    TokenDTO token;

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

    private async Task ContinueRevokeKey()
    {
        var newTokenKey = await dnsRepository.RevokeTokenKey(token.Id);

        if (!String.IsNullOrEmpty(newTokenKey))
        {
            token.Key = newTokenKey;
            isRevoking = false;
        }
    }

    private void ChangeRevokeStatus() => isRevoking = !isRevoking;

    private async Task OpenToken(TokenSummaryDTO record)
    {
        await TokenDetailsModal.Open();

        token = await dnsRepository.GetToken(record.Id);
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
