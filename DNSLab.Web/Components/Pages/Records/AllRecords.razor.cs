using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs.Zone;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Records;

partial class AllRecords
{
    [Inject] IZoneRepository _ZoneRepository { get; set; }
    [Inject] IRecordRepository _RecordRepository { get; set; }
    [Inject] IDialogService _DialogService { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }

    [Parameter] public Guid ZoneId { get; set; }

    ZoneDTO? _Zone { get; set; }
    IEnumerable<BaseRecordDTO>? _Records { get; set; }

    bool _IsLoading = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            _Zone = await _ZoneRepository.GetZone(ZoneId);
            _Records = await _RecordRepository.GetRecords(ZoneId);

            _IsLoading = false;
            await InvokeAsync(()=> StateHasChanged());
        }
    }

    Task Refresh()
    {
        return OnAfterRenderAsync(true);
    }

    async Task DisableRecord(BaseRecordDTO record)
    {
        bool _continue = true;

        if (record.Disable == false)
        {
            var parameters = new DialogParameters<BaseDialog>
                {
                    { x => x.ContentText, $"شما در حال غیر فعال سازی {record.Name}.{_Zone!.Name} میباشید آیا تایید میکنید؟" },
                    { x => x.ButtonText, "غیر فعال" },
                    { x => x.Color, Color.Warning }
                };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _DialogService.ShowAsync<BaseDialog>("غیر فعال سازی", parameters, options);
            var result = await dialog.Result;

            if (result!.Canceled)
            {
                _continue = false;
            }
        }

        if (_continue && await _RecordRepository.DisableRecord(record.Type, record.Id))
        {
            record.Disable = !record.Disable;

            _Snackbar.Add($"رکورد {record.Name}.{_Zone!.Name} {(record.Disable ? "غیر فعال" : "فعال")} شد", record.Disable ? Severity.Warning : Severity.Success);
        }
    }

    async Task DeleteRecord(BaseRecordDTO record)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {record.Name}.{_Zone!.Name} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            if (await _RecordRepository.DeleteRecord(record.Type, record.Id))
            {
                _Snackbar.Add($"رکورد {record.Name}.{_Zone!.Name} حذف شد", Severity.Success);
                await Refresh();
            }
        }
    }

    async Task NewRecord()
    {
        var parameters = new DialogParameters<RecordDialog>() { { "Zone", _Zone } };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<RecordDialog>("اضافه کردن رکورد جدید", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await Refresh();
        }
    }

    async Task EditRecord(BaseRecordDTO record)
    {
        var parameters = new DialogParameters<RecordDialog>() { { "Zone", _Zone }, { "Record", record.Clone() } };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<RecordDialog>("ویرایش رکورد", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await Refresh();
        }
    }
}
