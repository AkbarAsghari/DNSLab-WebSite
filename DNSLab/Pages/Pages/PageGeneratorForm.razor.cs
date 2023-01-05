using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Pages;
partial class PageGeneratorForm
{
    [Parameter] public string Title { get; set; }

    string selectedPageType
    {
        get
        {
            return Page.PageType.ToString();
        }
        set
        {
            if (value == null)
                value = PageTypeEnum.Article.ToString();
            Page.PageType = (PageTypeEnum)System.Enum.Parse(typeof(PageTypeEnum), value);
        }
    }

    private List<BitDropDownItem> GetPageTypeItems()
    {
        List<BitDropDownItem> result = new List<BitDropDownItem>();
        foreach (var item in Enum.GetValues(typeof(PageTypeEnum)))
            result.Add(new BitDropDownItem { Text = item.ToString()!, Value = item.ToString()! });
        return result;
    }

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
