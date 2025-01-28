using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Tickets;

partial class TicketMessages
{
    [Inject] ITicketRepository _TicketRepository { get; set; }

    [Parameter] public Guid TicketId { get; set; }

    TicketDTO? _Ticket { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _Ticket = await _TicketRepository.GetTicketMessages(TicketId);
            await InvokeAsync(StateHasChanged);
        }
    }
}
