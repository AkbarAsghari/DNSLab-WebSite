namespace DNSLab.Shared.Components.Page;
partial class PageLinkItem
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
    string _Link;
    [Parameter]
    public string Link
    {
        get
        {
            return _Link;
        }
        set
        {
            _Link = value.ToLower().StartsWith("site/") ? value.Substring(5, value.Length - 5) : value;
        }
    }
}
