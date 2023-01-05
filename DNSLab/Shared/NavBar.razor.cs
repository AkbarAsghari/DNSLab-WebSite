namespace DNSLab.Shared;
partial class NavBar
{
    bool collapseNavMenu = true;
    bool? _IsAuthenticated = null;
    string _FullName = String.Empty;
    string _UserID = String.Empty;

    string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    void CollapseNavMenu()
    {
        collapseNavMenu = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var user = (await auth.GetAuthenticationStateAsync()).User;

            if (user.Identity != null)
            {
                _IsAuthenticated = user.Identity!.IsAuthenticated == true;
                if (_IsAuthenticated == true)
                {
                    if (user.Identities.FirstOrDefault()!.Claims.Any(x => x.Type.ToLower() == "nameid"))
                        _UserID = user.Identities.FirstOrDefault()!.Claims.FirstOrDefault(x => x.Type.ToLower() == "nameid")!.Value;

                    if (user.Identities.FirstOrDefault()!.Claims.Any(x => x.Type.ToLower() == "fullname"))
                        _FullName = user.Identities.FirstOrDefault()!.Claims.FirstOrDefault(x => x.Type.ToLower() == "fullname")!.Value;

                }
            }
            else
            {
                _IsAuthenticated = false;
            }
            await this.InvokeAsync(() => StateHasChanged());
        }
    }
}
