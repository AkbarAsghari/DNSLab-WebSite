using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.DynamicDNSs;
using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs.Zone;
using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.DynamicDNSs;

partial class UpdateTokens
{
    [Inject] IDDNSRepository _DDNSRepository { get; set; }
    [Inject] IDialogService _DialogService { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }

    IEnumerable<UpdateTokenDTO>? _Tokens { get; set; }

    bool _IsLoading = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            await Refresh();

            _IsLoading = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }

    async Task Refresh()
    {
        _Tokens = await _DDNSRepository.GetTokens();
    }

    async Task NewToken()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<UpdateTokenDialog>("اضافه کردن کلید جدید", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            _Snackbar.Add("کلید جدید با موفقیت اضافه شد", Severity.Success);
            await Refresh();
        }
    }

    async Task EditToken(UpdateTokenDTO token)
    {
        var existToken = await _DDNSRepository.GetToken(token.Id);

        var parameters = new DialogParameters<UpdateTokenDialog>() { { "Token", existToken } };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<UpdateTokenDialog>("ویرایش کلید", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            _Snackbar.Add("کلید با موفقیت ویرایش شد", Severity.Success);
            await Refresh();
        }
    }

    async Task DeleteToken(UpdateTokenDTO token)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {token.Name} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            if (await _DDNSRepository.DeleteToken(token.Id))
            {
                _Snackbar.Add($"کلید {token.Name} حذف شد", Severity.Success);
                await Refresh();
            }
        }
    }

    async Task RevokeTokenKey(UpdateTokenDTO token)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال تغییر کلید {token.Name} میباشید آیا تایید میکنید؟ بعد از این عملیات دیگر امکان بروزرسانی با کلید قبلی وجود ندارد (کلید فعلی : {token.Key})" },
                { x => x.ButtonText, "تغییر کلید" },
                { x => x.Color, Color.Warning }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("تغییر کلید", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            var newKey = await _DDNSRepository.RevokeTokenKey(token.Id);
            if (!String.IsNullOrEmpty(newKey))
            {
                _Snackbar.Add($"کلید {token.Name} با موفقیت به {newKey} تغییر یافت", Severity.Success);
                token.Key = newKey;
            }
        }
    }
}
