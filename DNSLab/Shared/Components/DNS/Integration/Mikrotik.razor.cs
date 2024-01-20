using DNSLab.DTOs.DNS;
using DNSLab.Interfaces.Repository;
using DNSLab.Repository;
using MudBlazor;

namespace DNSLab.Shared.Components.DNS.Integration;

partial class Mikrotik
{
    IEnumerable<TokenSummaryDTO> tokens;
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IDNSRepository _DNSRepository { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    protected override async Task OnInitializedAsync()
    {
        tokens = await _DNSRepository.GetTokenSummary();
    }



    string TokenModalTitle { get; set; } = String.Empty;
    TokenAndDNSDTO TokenAndDNS { get; set; } = new TokenAndDNSDTO();


    private List<HostSummaryAndCheckedDTO> HostSummariesAndChecked;

    bool showEditOrAddTokenModal { get; set; } = false;

    private async Task EditTokenAsync(TokenSummaryDTO record)
    {

        showEditOrAddTokenModal = true;

        TokenAndDNS = await _DNSRepository.GetToken(record.Id);

        await CheckDNSRecords();

        await this.InvokeAsync(() => StateHasChanged());
    }

    private async Task CheckDNSRecords()
    {
        var HostSummaries = await _DNSRepository.GetHostSummaries();

        HostSummariesAndChecked = new List<HostSummaryAndCheckedDTO>();

        foreach (var item in HostSummaries)
            HostSummariesAndChecked.Add(new HostSummaryAndCheckedDTO
            {
                Id = item.Id,
                Address = item.Address,
                IsChecked = TokenAndDNS.HostNameIds.Contains(item.Id)
            });
    }

    private async Task AddNewToken()
    {
        showEditOrAddTokenModal = true;
        TokenAndDNS = new TokenAndDNSDTO() { HostNameIds = new List<Guid> { Guid.Empty } };
        await CheckDNSRecords();
    }

    private async Task DeleteTokenAsync(TokenSummaryDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
           "هشدار",
           $"آیا از حذف توکن {record.Name} مطمئن هستید؟",
           yesText: "حذف", cancelText: "انصراف");

        if (result == true)
        {
            if (await _DNSRepository.DeleteToken(record.Id))
            {
                await OnInitializedAsync();
            }
        }
    }

    bool ShowTokensOverlay = false;
    private TokenDTO Token;

    private async Task SelectToken(TokenSummaryDTO record)
    {
        Token = await _DNSRepository.GetToken(record.Id);
        ShowTokensOverlay = true;
    }
    private void EnabledTokensList()
    {
        Token = null;
        ShowTokensOverlay = false;
    }

    private async Task ContinueRevokeKey()
    {
        bool? result = await DialogService.ShowMessageBox(
           "هشدار",
           $"آیا از تغییر کلید توکن {Token.Name} مطمئن هستید؟",
           yesText: "تغییر", cancelText: "انصراف");

        if (result == true)
        {
            var newTokenKey = await _DNSRepository.RevokeTokenKey(Token.Id);

            if (!String.IsNullOrEmpty(newTokenKey))
            {
                Token.Key = newTokenKey;
            }
        }
    }

    private async Task SaveModalAsync()
    {
        TokenAndDNS.HostNameIds.Clear();
        HostSummariesAndChecked.Where(x => x.IsChecked == true).ToList().ForEach(x => TokenAndDNS.HostNameIds.Add(x.Id));

        bool isSuccess = false;
        if (TokenAndDNS.Id == Guid.Empty)
        {
            if (await _DNSRepository.GenerateTokenForAccessToUpdateHostNameSystem(TokenAndDNS))
            {
                Snackbar.Add("توکن با موفقیت ایجاد گردید", Severity.Success);
                isSuccess = true;
            }
        }
        else
        {
            if (await _DNSRepository.UpdateTokensDomainNameSystems(TokenAndDNS))
            {
                Snackbar.Add("توکن با موفقیت ویرایش شد", Severity.Success);
                isSuccess = true;
            }
        }

        if (isSuccess)
        {
            showEditOrAddTokenModal = false;
            await OnInitializedAsync();
        }
    }
}
