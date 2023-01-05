using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Articles;
partial class ArticleFormGenerator
{
    [Parameter] public string PageType { get; set; }
    [Parameter] public string URL { get; set; }

    PublishPageDTO publishPageDTO;
    string[] keywords;
    protected override async Task OnInitializedAsync()
    {
        publishPageDTO = await _PageRepository.GetPageByURL(url: $"{PageType}/{URL}");
        var pageMetadata = await _PageRepository.GetPageMetadata($"{PageType}/{URL}");

        keywords = pageMetadata.Keywords;
    }
}
