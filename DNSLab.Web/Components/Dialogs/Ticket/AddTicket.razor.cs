using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Ticket;

partial class AddTicket
{
    [CascadingParameter] MudDialogInstance _MudDialog { get; set; }

    [Inject] ITicketRepository _TicketRepository { get; set; }

    TicketDTO _NewTicket = new();

    string _Message = string.Empty;

    async Task Submit()
    {
        _NewTicket.Messages = new List<TicketMessageDTO>()
        {
            new()
            {
                Message = _Message,
            }
        };
        
        if (await _TicketRepository.AddTicket(_NewTicket))
        {
            _MudDialog.Close(DialogResult.Ok(true));
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
