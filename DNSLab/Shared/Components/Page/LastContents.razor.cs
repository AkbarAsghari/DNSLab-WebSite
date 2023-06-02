using DNSLab.DTOs.Pages;

namespace DNSLab.Shared.Components.Page;
partial class LastContents
{
    IEnumerable<LastContentsDTO> lastContents { get;set; }
    protected override async Task OnInitializedAsync()
    {
        lastContents = await _PageRepository.GetLastContents();
    }
}
