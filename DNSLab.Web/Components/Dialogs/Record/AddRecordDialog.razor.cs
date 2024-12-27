using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Record;

partial class AddRecordDialog
{
    [CascadingParameter] MudDialogInstance _MudDialog { get; set; }

    [Inject] IRecordRepository _RecordRepository { get; set; }

    [Parameter] public ZoneDTO Zone { get; set; }

    BaseRecordDTO _NewRecord = new() { Type = Enums.RecordTypeEnum.A };

    ARecordDTO _ARecord = new();
    AAAARecordDTO _AAARecord = new();
    TXTRecordDTO _TXTRecord = new();
    CNAMERecordDTO _CNAMERecord = new();
    MXRecordDTO _MXRecord = new();

    async Task Submit()
    {
        switch (_NewRecord.Type)
        {
            case Enums.RecordTypeEnum.A:
                _NewRecord.RData = _ARecord;
                break;
            case Enums.RecordTypeEnum.AAAA:
                _NewRecord.RData = _AAARecord;
                break;
            case Enums.RecordTypeEnum.TXT:
                _NewRecord.RData = _TXTRecord;
                break;
            case Enums.RecordTypeEnum.CNAME:
                _NewRecord.RData = _CNAMERecord;
                break;
            case Enums.RecordTypeEnum.MX:
                _NewRecord.RData = _MXRecord;
                break;
            default:
                break;
        }

        if (await _RecordRepository.AddRecord(Zone.Id, _NewRecord))
        {
            _MudDialog.Close(DialogResult.Ok(true));
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
