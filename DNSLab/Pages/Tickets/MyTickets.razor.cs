using DNSLab.DTOs.Ticket;

namespace DNSLab.Pages.Tickets;
partial class MyTickets
{
    IEnumerable<TicketDTO> tickets;

    Modal Modal { get; set; }
    TicketDTO deleteRcord { get; set; } = new();
    TicketDTO closeRcord { get; set; } = new();
    string modalTitle = String.Empty;

    protected override async Task OnInitializedAsync()
    {
        tickets = await ticketRepository.GetTickets();
    }

    async Task DeleteTicket(TicketDTO record)
    {
        DeSelectAll();
        deleteRcord = record;
        modalTitle = localizer["AreYouSureDel"];
        await Modal.Open();
    }

    async Task CloseTicket(TicketDTO record)
    {
        DeSelectAll();
        closeRcord = record;
        modalTitle = localizer["AreYouSureClose"];
        await Modal.Open();
    }

    async Task ModalAccept()
    {
        if (deleteRcord.ID != Guid.Empty) await AcceptDelete();
        if (closeRcord.ID != Guid.Empty) await AcceptClose();
    }

    void DeSelectAll()
    {
        deleteRcord = new();
        closeRcord = new();
    }

    async Task AcceptDelete()
    {
        if (deleteRcord != null)
        {
            if (await ticketRepository.RemoveTicket(deleteRcord.ID))
            {
                await Modal.Close();
                await OnInitializedAsync();
            }
        }
    }

    void OpenTicket(TicketDTO record)
    {
        navigationManager.NavigateTo("Ticket/Details/" + record.ID);
    }

    async Task AcceptClose()
    {
        if (closeRcord != null)
        {
            if (await ticketRepository.CloseTicket(closeRcord.ID))
            {
                await Modal.Close();
                await OnInitializedAsync();
            }
        }
    }
}
