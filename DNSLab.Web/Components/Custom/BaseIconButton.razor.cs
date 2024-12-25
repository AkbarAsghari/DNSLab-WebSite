using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Custom;

partial class BaseIconButton
{
    [Parameter]
    public EditContext EditContext { get; set; }
    [Parameter]
    public MudForm Form { get; set; }

    protected override async Task OnClickHandler(MouseEventArgs ev)
    {
        base.Disabled = true;

        bool invoke = false;

        if (EditContext == null && Form == null)
        {
            invoke = true;
        }
        else if (Form != null)
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                invoke = true;
            }
        }
        else if (EditContext != null)
        {
            if (EditContext.Validate())
            {
                invoke = true;
            }
        }

        if (invoke)
        {
            await OnClick.InvokeAsync();
        }

        base.Disabled = false;
        await InvokeAsync(() => StateHasChanged());
    }
}
