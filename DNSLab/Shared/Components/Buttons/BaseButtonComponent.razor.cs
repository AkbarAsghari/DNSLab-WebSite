using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace DNSLab.Shared.Components.Buttons;
partial class BaseButtonComponent
{
    [Parameter] public Color Color { get; set; } = Color.Primary;
    [Parameter] public string? Icon { get; set; } = String.Empty;
    [Parameter] public string Caption { get; set; }
    [Parameter] public bool IsOutLine { get; set; }
    [Parameter] public bool IsBussy { get; set; }
    [Parameter] public string IsBussyCaption { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter] public EditContext EditContext { get; set; }

    private async Task ButtonClickedEvent()
    {
        IsBussy = true;

        if (EditContext == null)
            await OnClick.InvokeAsync();
        else
        if (EditContext.Validate())
            await OnClick.InvokeAsync();

        IsBussy = false;
    }
}
