using Microsoft.JSInterop;

namespace DNSLab.Shared.Components.TextEditor;
partial class TextEditor
{
    string _id;
    private string _HTML;

    [Parameter]
    public string Id
    {
        get => _id ?? $"CKEditor_{uid}";
        set => _id = value;
    }

    readonly string uid = Guid.NewGuid().ToString().ToLower().Replace("-", "");

    protected override void OnInitialized()
    {
        _HTML = CurrentValue;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync(identifier: "import", "/js/ckeditor/ckeditor.js");
            await JSRuntime.InvokeVoidAsync(identifier: "import", "/js/ckeditor/CKEditorInterop.js");

            await JSRuntime.InvokeVoidAsync("CKEditorInterop.init", Id, DotNetObjectReference.Create(this));
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    [JSInvokable]
    public Task EditorDataChanged(string data)
    {
        CurrentValue = data;
        StateHasChanged();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        JSRuntime.InvokeVoidAsync("CKEditorInterop.destroy", Id);
    }
}
