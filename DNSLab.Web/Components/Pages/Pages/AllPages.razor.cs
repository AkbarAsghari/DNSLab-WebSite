using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Pages;

partial class AllPages
{
    [Inject] IPageRepository _PageRepository { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    IEnumerable<PageInfoDTO>? _Pages { get; set; }
    bool _IsLoading = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            _Pages = await _PageRepository.GetAllPages();

            _IsLoading = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }

    async Task DeletePage(PageInfoDTO page)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {page.Title} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            if (await _PageRepository.DeletePage(page.Id))
            {
                _Snackbar.Add($"صفحه {page.Title} حذف شد", Severity.Success);
                await OnAfterRenderAsync(true);
            }
        }
    }
}
