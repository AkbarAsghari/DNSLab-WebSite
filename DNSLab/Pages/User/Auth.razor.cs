using DNSLab.DTOs.User;
using MudBlazor;

namespace DNSLab.Pages.User;
partial class Auth
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string? RedirectTo { get; set; }

    private AuthenticateDTO user = new AuthenticateDTO();

    protected override void OnInitialized()
    {
        if (!String.IsNullOrWhiteSpace(RedirectTo) && !RedirectTo.ToLower().EndsWith("dashboard"))
            Snackbar.Add(localizer["PleaseLoginFirst"], Severity.Info);
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
