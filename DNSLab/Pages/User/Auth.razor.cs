using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Auth
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string? RedirectTo { get; set; }

    private AuthenticateDTO user = new AuthenticateDTO();

    protected override async Task OnInitializedAsync()
    {
        if (!String.IsNullOrWhiteSpace(RedirectTo) && !RedirectTo.ToLower().EndsWith("dashboard"))
            _toastService.ShowToast(localizer["PleaseLoginFirst"], Enums.ToastLevel.Info);
    }

    private async Task AuthUser()
    {
        var userToken = await accountReository.Login(user);
        if (!String.IsNullOrEmpty(userToken))
        {
            navigationManager.NavigateTo(RedirectTo == null ? "dashboard" : RedirectTo);
            await authService.Login(userToken);
        }
    }
}
