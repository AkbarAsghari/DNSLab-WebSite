using Microsoft.AspNetCore.Components.Routing;

namespace DNSLab.Shared;
partial class URLChangeDetection
{
    protected override async Task OnInitializedAsync()
    {
        await _IStaticsRepository.PageVisit(httpContextAccessor.HttpContext.Connection?.RemoteIpAddress.ToString(), new Uri(_NavigationManager.Uri).LocalPath);
        _NavigationManager.LocationChanged += _navigationManager_LocationChanged;
    }

    private async void _navigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        await _IStaticsRepository.PageVisit(httpContextAccessor.HttpContext.Connection?.RemoteIpAddress.ToString(), new Uri(_NavigationManager.Uri).LocalPath);
    }

    public void Dispose()
    {
        _NavigationManager.LocationChanged -= _navigationManager_LocationChanged;
    }
}
