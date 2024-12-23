using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Providers;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class SignOut
{
    [Inject] IAuthenticationProvider _AuthenticationProvider { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await _AuthenticationProvider.Logout();
        _NavigationManager.NavigateTo("/");
    }
}
