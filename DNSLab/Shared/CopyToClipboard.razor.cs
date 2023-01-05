using Microsoft.JSInterop;

namespace DNSLab.Shared;
partial class CopyToClipboard
{
    bool isCopied = false;
    [Parameter] public string Text { get; set; }

    private async Task CopyTextToClipboard()
    {
        isCopied = await JSRuntime.InvokeAsync<bool>("clipboardCopy.copyText", Text);
    }
}
