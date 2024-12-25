
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.Zone;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using MudBlazor.Charts;
using static MudBlazor.CategoryTypes;

namespace DNSLab.Web.Components.Pages.Zone
{
    partial class AllZones
    {
        [Inject] IZoneRepository _ZoneRepository { get; set; }
        [Inject] ISnackbar _Snackbar { get; set; }
        [Inject] IDialogService _DialogService { get; set; }

        MudDataGrid<ZoneDTO> _DataGrid { get; set; }

        private async Task<GridData<ZoneDTO>> ServerReload(GridState<ZoneDTO> state)
        {
            var data = await _ZoneRepository.GetZones();

            if (data == null)
            {
                return new GridData<ZoneDTO>();
            }

            var sortDefinition = state.SortDefinitions.FirstOrDefault();
            if (sortDefinition != null)
            {
                switch (sortDefinition.SortBy)
                {
                    case nameof(ZoneDTO.Name):
                        data = data.OrderByDirection(
                            sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
                            o => o.Name
                        );
                        break;
                }
            }
            else
            {
                //data = data.OrderByDirection(SortDirection.Descending, o => o.CreateDate);
            }

            return new GridData<ZoneDTO>
            {
                TotalItems = data.Count(),
                Items = data
            };
        }

        async Task NewZone()
        {
            var options = new DialogOptions() { CloseButton = true, FullWidth = true , MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _DialogService.ShowAsync<AddZoneDialog>("اضافه کردن Zone جدید", options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                await _DataGrid.ReloadServerData();
            }
        }

        async Task EditZone(ZoneDTO zone)
        {

        }

        async Task DeleteZone(ZoneDTO zone)
        {
            var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {zone.Name} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                if (await _ZoneRepository.DeleteZone(zone.Id))
                {
                    _Snackbar.Add($"دامنه {zone.Name} حذف شد", Severity.Success);
                    await _DataGrid.ReloadServerData();
                }
            }
        }

        async Task DisableZone(ZoneDTO zone)
        {
            bool _continue = true;

            if (zone.Disable == false)
            {
                var parameters = new DialogParameters<BaseDialog>
                {
                    { x => x.ContentText, $"شما در حال غیر فعال سازی {zone.Name} میباشید آیا تایید میکنید؟" },
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

            if (_continue && await _ZoneRepository.DisableZone(zone.Id))
            {
                zone.Disable = !zone.Disable;

                _Snackbar.Add($"دامنه {zone.Name} {(zone.Disable ? "غیر فعال" : "فعال")} شد", zone.Disable ? Severity.Warning : Severity.Success);
            }
        }
    }
}
