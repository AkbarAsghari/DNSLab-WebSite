using DNSLab.DTOs.Pages;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Components.Pages.SiteChanges;
partial class ChangeLog
{
    [Inject] IPageRepository _PageRepository { get; set; }

    [Parameter] public string URL { get; set; }

    ChangeLogDTO _ChangeLog;
    protected override async Task OnInitializedAsync()
    {
        _ChangeLog = await _PageRepository.GetChangeLogByURL(url: URL);
    }
}
