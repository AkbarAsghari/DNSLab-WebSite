using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Pages;
partial class PageGeneratorForm
{
    [Parameter] public string Title { get; set; }

    [Parameter]
    public PageDTO Page { get; set; } = new PageDTO
    {
        URL = String.Empty,
        Title = String.Empty,
        Description = String.Empty,
        Keywords = new List<string>(),
        PageType = PageTypeEnum.Article,
        Body = "مطلب خود را در اینجا بنویسید.",
    };

    [Parameter] public EventCallback OnValidSubmit { get; set; }
}
