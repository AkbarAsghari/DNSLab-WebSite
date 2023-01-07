using DNSLab.Enums.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DNSLab.Shared.Components.Buttons;
partial class BaseButtonComponent
{
    [Parameter] public string Class { get; set; }
    [Parameter] public ColorEnum Color { get; set; }
    [Parameter] public BitIconName? Icon { get; set; }
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
