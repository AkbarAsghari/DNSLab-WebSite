using DNSLab.DTOs.Dashboard;

namespace DNSLab.Pages.Defaults;
partial class Dashboard
{
    DashboardDTO _Dashboard;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _Dashboard = await dashboardRepository.GetStats();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }
}
