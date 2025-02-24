using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Zone;

partial class AddZoneDialog
{
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    [Inject] IZoneRepository _ZoneRepository { get; set; }

    ZoneDTO _NewZone = new();

    async Task Submit()
    {
        if (await _ZoneRepository.CreateZone(_NewZone))
        {
            _MudDialog.Close(DialogResult.Ok(true));
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
