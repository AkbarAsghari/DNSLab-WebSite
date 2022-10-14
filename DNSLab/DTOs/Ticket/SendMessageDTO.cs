using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Ticket
{
    public class SendMessageDTO
    {
        public Guid TicketId { get; set; }
        public string Text { get; set; }
    }
}
