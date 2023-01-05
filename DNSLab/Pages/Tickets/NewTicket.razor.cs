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
            toastService.ShowToast(localizer["TicketSent"], Enums.ToastLevel.Success);
            _navManager.NavigateTo("Ticket/MyTickets");
        }
        else
            toastService.ShowToast(localizer["UnableToSendTicket"], Enums.ToastLevel.Error);

    }
}
