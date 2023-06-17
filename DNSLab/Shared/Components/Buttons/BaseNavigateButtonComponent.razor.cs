using MudBlazor;

namespace DNSLab.Shared.Components.Buttons;
partial class BaseNavigateButtonComponent
{
    [Parameter] public string Href { get; set; }
    [Parameter] public string Target { get; set; } = "_self";
    [Parameter] public string Text { get; set; }
    [Parameter] public string? Icon { get; set; } = String.Empty;
    [Parameter] public Color Color { get; set; } = Color.Primary;
}
