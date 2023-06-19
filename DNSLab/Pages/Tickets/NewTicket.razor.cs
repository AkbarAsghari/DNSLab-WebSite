using DNSLab.DTOs.Ticket;

namespace DNSLab.Pages.Tickets;
partial class NewTicket
{
    private NewTicketDTO ticket = new() { Title = String.Empty, Text = String.Empty };
    const int MAX_TEXT_COUNT = 2000;

    async void SendNewTicket()
    {
        if (await ticketRepository.AddTicket(ticket))
        {
            Snackbar.Add(localizer["TicketSent"], MudBlazor.Severity.Success);
            _navManager.NavigateTo("Ticket/MyTickets");
        }
        else
            Snackbar.Add(localizer["UnableToSendTicket"], MudBlazor.Severity.Error);

    }
}
