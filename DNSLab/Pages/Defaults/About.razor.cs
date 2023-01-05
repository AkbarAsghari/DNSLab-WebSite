namespace DNSLab.Pages.Defaults;
partial class About
{
    private bool isLoadingUsersCount = true;
    private bool isLoadingDNSCount = true;
    private bool isLoadingTodayIPChangesCount = true;

    int usersCount = 0;
    int dNSCount = 0;
    int todayIPChangesCount = 0;

    protected override async Task OnInitializedAsync()
    {
        await Task.WhenAll(
            LoadUsersCount(),
            LoadActiveDNSCount(),
            LoadTodayIPChangesCount());
    }

    private async Task LoadUsersCount()
    {
        usersCount = await accountReository.UsersCount();
        isLoadingUsersCount = false;
    }

    private async Task LoadActiveDNSCount()
    {
        dNSCount = await dnsRepository.GetAllUsersDNSCount();
        isLoadingDNSCount = false;
    }

    private async Task LoadTodayIPChangesCount()
    {
        todayIPChangesCount = await dnsRepository.GetTodayIPChangesCount();
        isLoadingTodayIPChangesCount = false;
    }
}
