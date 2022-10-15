using DNSLab.DTOs.Ticket;

namespace DNSLab.Interfaces.Repository
{
    public interface ITicketRepository
    {
        Task<bool> AddTicket(NewTicketDTO newTicket);
        Task<bool> RemoveTicket(Guid ticketId);
        Task<TicketDTO> Get(Guid ticketId);
        Task<IEnumerable<MessageDTO>> GetTicketMessages(Guid ticketId);
        Task<bool> SendMessage(SendMessageDTO message);
        Task<bool> CloseTicket(Guid ticketId);
        Task<IEnumerable<TicketDTO>> GetTickets(bool? isClosed = null);
    }
}
