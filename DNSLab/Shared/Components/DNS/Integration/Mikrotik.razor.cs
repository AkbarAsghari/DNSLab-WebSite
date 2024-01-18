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
        ShowTokensOverlay = false;
    }

    bool isRevoking = false;
    private void ChangeRevokeStatus() => isRevoking = !isRevoking;

    private async Task ContinueRevokeKey()
    {
        var newTokenKey = await _DNSRepository.RevokeTokenKey(Token.Id);

        if (!String.IsNullOrEmpty(newTokenKey))
        {
            Token.Key = newTokenKey;
            isRevoking = false;
        }
    }

    private void SaveModal()
    {

    }
}
