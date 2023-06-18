using DNSLab.DTOs.Ticket;

namespace DNSLab.Pages.Tickets;
partial class TIcketDetails
{
    [Parameter] public Guid TicketId { get; set; }
    const int MAX_TEXT_COUNT = 2000;
    SendMessageDTO Message { get; set; } = new SendMessageDTO { Text = String.Empty };

    ICollection<MessageDTO> messages;
    TicketDTO ticket;

    Guid CurrentUserId;

    async Task SendTicket()
    {
        Message.TicketId = TicketId;

        if (await ticketRepository.SendMessage(Message))
        {
            messages.Add(new MessageDTO
            {
                UserId = CurrentUserId,
                CreateDate = DateTime.UtcNow,
                Text = Message.Text,
            });

            Message = new SendMessageDTO { Text = String.Empty };
        }
    }

    protected override async Task OnInitializedAsync()
    {
        ticket = await ticketRepository.Get(TicketId);
        messages = (await ticketRepository.GetTicketMessages(TicketId)).ToList();

        CurrentUserId = Guid.Parse((await auth.GetAuthenticationStateAsync()).User.Identities.FirstOrDefault()!.Claims.FirstOrDefault(x => x.Type.ToLower() == "nameid")!.Value);
    }
}
