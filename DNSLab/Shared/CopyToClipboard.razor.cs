using Microsoft.JSInterop;

namespace DNSLab.Shared;
partial class CopyToClipboard
{
    bool isCopied = false;
    [Parameter] public string Text { get; set; }

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
