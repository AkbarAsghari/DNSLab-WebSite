using Microsoft.JSInterop;

namespace DNSLab.Shared;
partial class CopyToClipboard
{
    bool isCopied = false;
    [Parameter] public string Text { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync(identifier: "import", "/js/copyToClipboard.js");
        }

        await base.OnAfterRenderAsync(firstRender);
    }
    private async Task CopyTextToClipboard(bool toggled)
    {
        if (toggled)
        {
            isCopied = await JSRuntime.InvokeAsync<bool>("clipboardCopy.copyText", Text);
        }
        else
        {
            isCopied = false;
        }
    }
}
