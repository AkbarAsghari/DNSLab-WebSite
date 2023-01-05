namespace DNSLab.Shared.Components.Page;
partial class PageVisitCounter
{
    int visitCount = -1;
    protected override async Task OnInitializedAsync()
    {
        visitCount = await _StaticsRepository.PageVisitCount(new Uri(_NavigationManager.Uri).LocalPath);
        await this.InvokeAsync(() => StateHasChanged());
    }
}
