namespace DNSLab.Pages.Defaults;
partial class Dashboard
{
    private bool isLoadingUsersCount = true;
    private bool isLoadingActiveDNSCount = true;
    private bool isLoadingLast24HoursChangesCount = true;

    int usersCount = 0;
    int activeDNSCount = 0;
    int last24HoursChangesCount = 0;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var user = (await auth.GetAuthenticationStateAsync()).User;
            if (user.Identity?.IsAuthenticated == true)
            {
                await Task.WhenAll(LoadUsersCount(), LoadActiveDNSCount(), LoadLast24HoursChangesCount());
                await this.InvokeAsync(() => StateHasChanged());
            }
        }
    }

    private async Task LoadUsersCount()
    {
        isLoadingUsersCount = true;
        usersCount = await accountReository.UsersCount();
        isLoadingUsersCount = false;
    }

    private async Task LoadActiveDNSCount()
    {
        isLoadingActiveDNSCount = true;
        activeDNSCount = await dnsRepository.GetActiveDNSCount();
        isLoadingActiveDNSCount = false;
    }

    private async Task LoadLast24HoursChangesCount()
    {
        isLoadingLast24HoursChangesCount = true;
        last24HoursChangesCount = await dnsRepository.GetLast24HoursChangesCount();
        isLoadingLast24HoursChangesCount = false;
    }
}
