using DNSLab.DTOs.IP;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Components.Pages.Defaults;
partial class About
{
    [Inject] IRealTimeCommunicationRepository _RealTimeCommunicationRepository { get; set; }
    [CascadingParameter] public IPDTO IPDTO { get; set; }

    private bool isLoadingUsersCount = true;
    private bool isLoadingDNSCount = true;
    private bool isLoadingTodayIPChangesCount = true;
    private bool isLoadingGetActiveSubscriptionCount = true;

    int usersCount = 0;
    int dNSCount = 0;
    int todayIPChangesCount = 0;
    int activeSubscriptionCount = 0;
    int onlineUsersCount = 0;

    protected override async Task OnInitializedAsync()
    {
        _RealTimeCommunicationRepository.OnUpdateUsersCount += OnlineUsersCountUpdate;
        await _RealTimeCommunicationRepository.StartListening(IPDTO.IPv4);

        await Task.WhenAll(
                LoadUsersCount(),
                LoadActiveDNSCount(),
                LoadTodayIPChangesCount(),
                LoadGetActiveSubscriptionCount());
    }

    private void OnlineUsersCountUpdate(int count)
    {
        onlineUsersCount = count;
        this.InvokeAsync(() => StateHasChanged());
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

    private async Task LoadGetActiveSubscriptionCount()
    {
        activeSubscriptionCount = await subscriptionsRepository.GetActiveSubscriptionCount();
        isLoadingGetActiveSubscriptionCount = false;
    }
}
