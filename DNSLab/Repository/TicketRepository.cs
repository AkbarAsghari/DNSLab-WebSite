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

        public async Task<TicketDTO> Get(Guid ticketId)
        {
            var response = await _httpService.Get<TicketDTO>($"/Ticket/Get?ticketId={ticketId}");
            return response.Response;
        }

        public async Task<IEnumerable<MessageDTO>> GetTicketMessages(Guid ticketId)
        {
            var response = await _httpService.Get<IEnumerable<MessageDTO>>($"/Ticket/GetTicketMessages?ticketId={ticketId}");
            return response.Response;
        }

        public async Task<IEnumerable<TicketDTO>> GetTickets(bool? isClosed = null)
        {
            var response = await _httpService.Get<IEnumerable<TicketDTO>>($"/Ticket/GetTickets?isClosed={isClosed}");

            if (response.Response == null)
                return new List<TicketDTO>();

            return response.Response;
        }

        public async Task<bool> RemoveTicket(Guid ticketId)
        {
            var response = await _httpService.Delete<bool>($"/Ticket/RemoveTicket?ticketId={ticketId}");
            if (!response.Success)
                return false;
            else
                return response.Response;
        }

        public async Task<bool> SendMessage(SendMessageDTO message)
        {
            var response = await _httpService.Post<SendMessageDTO, bool>($"/Ticket/SendMessage", message);
            if (!response.Success)
                return false;
            else
                return response.Response;
        }
    }
}
