using DNSLab.Web.DTOs.Repositories.Ticket;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<bool> AddTicket(TicketDTO model);
        Task<bool> DeleteTicket(Guid id);
        Task<bool> AddTicketMessage(TicketMessageDTO model);
        Task<List<TicketDTO>?> GetAllTickets();
        Task<List<TicketDTO>?> GetMyTickets();
        Task<TicketDTO?> GetTicketMessages(Guid ticketId);
    }
}
