using DNSLab.DTOs.DNS;
using MudBlazor;

namespace DNSLab.Pages.DNS;
partial class MyTokens
{
    [Inject] private IDialogService DialogService { get; set; }
    IEnumerable<TokenSummaryDTO> tokenSummaries;

    private bool isRevoking = false;
    bool showTokenDetails = false;

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
        bool? result = await DialogService.ShowMessageBox(
            "هشدار",
            $"آیا از حذف توکن {record.Name} مطمئن هستید؟",
            yesText: "حذف", cancelText: "انصراف");

        if (result == true)
        {
            if (await dnsRepository.DeleteToken(record.Id))
            {
                await LoadTokensSummary();
            }
        }
    }

    private void EditToken(TokenSummaryDTO record)
    {
        NavigationManager.NavigateTo("dns/token/edit/" + record.Id);
    }
}
