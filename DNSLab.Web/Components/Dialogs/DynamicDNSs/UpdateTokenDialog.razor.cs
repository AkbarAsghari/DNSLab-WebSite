using DNSLab.Web.Components.Pages.Records;
using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.DynamicDNSs;

partial class UpdateTokenDialog
{
    [CascadingParameter] MudDialogInstance _MudDialog { get; set; }

    [Inject] IDDNSRepository _DDNSRepository { get; set; }

    [Parameter] public UpdateTokenAndRecordsDTO Token { get; set; } = new() { Records = new() };

    IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>? _AllRecords { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _AllRecords = await _DDNSRepository.GetDDNSDomainAndRecordsForToken();
    }

    void RecordCheckChange(Guid recordId, bool check)
    {
        if (check && !Token.Records.Contains(recordId))
        {
            Token.Records.Add(recordId);
        }
        else
        {
            Token.Records.Remove(recordId);
        }
    }

    async Task Submit()
    {
        if (Token.Id == Guid.Empty)
        {
            if (await _DDNSRepository.AddToken(Token))
            {
                _MudDialog.Close(DialogResult.Ok(true));
            }
        }
        else
        {
            if (await _DDNSRepository.UpdateToken(Token))
            {
                _MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
