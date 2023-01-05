namespace DNSLab.Shared.Components.Page;
partial class PageSummary
{
    int visitCount = -1;

    [Parameter] public Guid UserId { get; set; }
    [Parameter] public string FullName { get; set; } = String.Empty;
    [Parameter] public DateTime? PublishDate { get; set; }
    [Parameter] public DateTime? UpdateDate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        visitCount = await _StaticsRepository.PageVisitCount(new Uri(_NavigationManager.Uri).LocalPath);
    }
}
