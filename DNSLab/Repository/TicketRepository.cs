using DNSLab.DTOs.Ticket;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IHttpService _httpService;

        public TicketRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> AddTicket(NewTicketDTO newTicket)
        {
            var response = await _httpService.Post<NewTicketDTO, bool>($"/Ticket/NewTicket", newTicket);
            if (!response.Success)
                return false;
            else
                return response.Response;
        }

        public async Task<bool> CloseTicket(Guid ticketId)
        {
            var response = await _httpService.Post<bool>($"/Ticket/CloseTicket?ticketId={ticketId}");
            if (!response.Success)
                return false;
            else
                return response.Response;
        }

        public async Task<IEnumerable<MessageDTO>> GetTicketMessages(Guid ticketId)
        {
            var response = await _httpService.Get<IEnumerable<MessageDTO>>($"/Ticket/GetTicketMessages?ticketId={ticketId}");
            return response.Response;
        }

        public async Task<IEnumerable<TicketDTO>> GetTickets(bool isClosed)
        {
            var response = await _httpService.Get<IEnumerable<TicketDTO>>($"/Ticket/GetTickets?isClosed={isClosed}");
            return response.Response;
        }

        public async Task<bool> RemoveTicket(Guid ticketId)
        {
            var response = await _httpService.Post<bool>($"/Ticket/CloseTicket?ticketId={ticketId}");
            if (!response.Success)
                return false;
            else
                return response.Response;
        }

        public async Task<bool> SendMessage(SendMessageDTO message)
        {
            var response = await _httpService.Post<SendMessageDTO, bool>($"/Ticket/NewTicket", message);
            if (!response.Success)
                return false;
            else
                return response.Response;
        }
    }
}
