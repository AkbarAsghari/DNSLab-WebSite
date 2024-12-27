﻿using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Record;

partial class RecordDialog
{
    [CascadingParameter] MudDialogInstance _MudDialog { get; set; }

    [Inject] IRecordRepository _RecordRepository { get; set; }

    [Parameter] public ZoneDTO Zone { get; set; }
    [Parameter] public BaseRecordDTO Record { get; set; } = new() { Type = Enums.RecordTypeEnum.A };

    ARecordDTO _ARecord = new();
    AAAARecordDTO _AAARecord = new();
    TXTRecordDTO _TXTRecord = new();
    CNAMERecordDTO _CNAMERecord = new();
    MXRecordDTO _MXRecord = new();

    protected override void OnInitialized()
    {
        if (Record.RData != null)
        {
            switch (Record.Type)
            {
                case Enums.RecordTypeEnum.A:
                    _ARecord = Record.CastTo<ARecordDTO>()!;
                    break;
                case Enums.RecordTypeEnum.AAAA:
                    _AAARecord = Record.CastTo<AAAARecordDTO>()!;
                    break;
                case Enums.RecordTypeEnum.TXT:
                    _TXTRecord = Record.CastTo<TXTRecordDTO>()!;
                    break;
                case Enums.RecordTypeEnum.CNAME:
                    _CNAMERecord = Record.CastTo<CNAMERecordDTO>()!;
                    break;
                case Enums.RecordTypeEnum.MX:
                    _MXRecord = Record.CastTo<MXRecordDTO>()!;
                    break;
                default:
                    break;
            }
        }
    }

    async Task Submit()
    {
        switch (Record.Type)
        {
            case Enums.RecordTypeEnum.A:
                Record.RData = _ARecord;
                break;
            case Enums.RecordTypeEnum.AAAA:
                Record.RData = _AAARecord;
                break;
            case Enums.RecordTypeEnum.TXT:
                Record.RData = _TXTRecord;
                break;
            case Enums.RecordTypeEnum.CNAME:
                Record.RData = _CNAMERecord;
                break;
            case Enums.RecordTypeEnum.MX:
                Record.RData = _MXRecord;
                break;
            default:
                break;
        }

        if (Record.Id == Guid.Empty)
        {
            if (await _RecordRepository.AddRecord(Zone.Id, Record))
            {
                _MudDialog.Close(DialogResult.Ok(true));
            }
        }
        else
        {
            if (await _RecordRepository.UpdateRecord(Record))
            {
                _MudDialog.Close(DialogResult.Ok(true));
            }
        }

    }

    private void Cancel() => _MudDialog.Cancel();
}
