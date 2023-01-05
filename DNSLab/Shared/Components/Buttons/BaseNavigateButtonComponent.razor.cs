namespace DNSLab.Shared.Components.Buttons;
partial class BaseNavigateButtonComponent
{
    [Parameter] public string Href { get; set; }
    [Parameter] public string Target { get; set; } = "_self";
    [Parameter] public string Text { get; set; }
    [Parameter] public BitIconName? Icon { get; set; }
    void GoTo()
    {
        _NavigationManager.NavigateTo(Href);
    }
}
