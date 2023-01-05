using System.ComponentModel;

namespace DNSLab.Shared;
partial class Metadata
{
    bool isComplate = false;
    protected override async Task OnInitializedAsync()
    {
        await MetadataTransferService.Start();
        MetadataTransferService.PropertyChanged += OnMetadataChanged!;
        isComplate = true;
    }

    private void OnMetadataChanged(object sender, PropertyChangedEventArgs e) => this.InvokeAsync(() => StateHasChanged());

    public void Dispose() => MetadataTransferService.PropertyChanged -= OnMetadataChanged!;
}
