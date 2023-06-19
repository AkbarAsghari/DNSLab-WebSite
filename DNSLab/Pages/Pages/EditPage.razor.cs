using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Pages;
partial class EditPage
{
    [Parameter] public Guid PageId { get; set; }

    private PageDTO existPage;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            existPage = await _PageRepository.GetPage(PageId);
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    public async Task Update()
    {
        if (await _PageRepository.EditPage(existPage))
        {
            _navManager.NavigateTo("Page/MyPages");
            Snackbar.Add(localizer["PageUpdated"], MudBlazor.Severity.Success);
        }
    }
}
