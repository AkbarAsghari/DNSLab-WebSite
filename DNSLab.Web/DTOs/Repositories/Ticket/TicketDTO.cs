using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Ticket
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public TicketStatusDTO? Status { get; set; } = default!;
        public List<TicketMessageDTO> Messages { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
