using DNSLab.DTOs.Pages;

namespace DNSLab.Components.Pages.Pages;

partial class PageLoader
{
    [Parameter] public string PageType { get; set; }
    [Parameter] public string URL { get; set; }

    PublishPageDTO publishPageDTO;
    string[] keywords;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            publishPageDTO = await _PageRepository.GetPageByURL(url: $"{PageType}/{URL}");
            var pageMetadata = await _PageRepository.GetPageMetadata($"{PageType}/{URL}");

            keywords = pageMetadata.Keywords;
        }
    }
}
