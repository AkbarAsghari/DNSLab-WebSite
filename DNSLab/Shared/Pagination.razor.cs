using DNSLab.Resources;
using Microsoft.Extensions.Localization;

namespace DNSLab.Shared;
partial class Pagination
{
    [Inject] IStringLocalizer<Resource> localizer { get; set; }

    [Parameter] public int CurrentPage { get; set; } = 1;
    [Parameter] public int TotalAmountPages { get; set; }
    [Parameter] public int Radius { get; set; } = 3;
    [Parameter] public EventCallback<int> SelectedPage { get; set; }
    List<LinkModel> links;

    bool isMultiPage = true;

    private async Task SelectedPageInternal(int selected)
    {
        CurrentPage = selected;
        await SelectedPage.InvokeAsync(selected);
    }

    protected override void OnParametersSet()
    {
        LoadPages();
        base.OnParametersSet();
    }

    private void LoadPages()
    {
        links = new List<LinkModel>();
        var isPreviousPageLinkEnabled = CurrentPage != 1;
        var previousPage = CurrentPage - 1;
        links.Add(new LinkModel(previousPage, isPreviousPageLinkEnabled, localizer["Previous"]));

        for (int i = 1; i <= TotalAmountPages; i++)
        {
            if (i >= CurrentPage - Radius && i <= CurrentPage + Radius)
            {
                links.Add(new LinkModel(i) { Active = CurrentPage == i });
            }
        }

        var isNextPageLinkEnabled = CurrentPage != TotalAmountPages;
        var nextPage = CurrentPage + 1;
        links.Add(new LinkModel(nextPage, isNextPageLinkEnabled, localizer["Next"]));

        isMultiPage = isNextPageLinkEnabled ? true : isPreviousPageLinkEnabled;
    }

    class LinkModel
    {
        public LinkModel(int page) : this(page, true) { }
        public LinkModel(int page, bool enabled) : this(page, true, page.ToString()) { }
        public LinkModel(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }

        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; } = true;
        public bool Active { get; set; } = false;
    }
}
