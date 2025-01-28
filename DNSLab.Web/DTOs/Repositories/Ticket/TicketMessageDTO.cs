using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Ticket
{
    public class TicketMessageDTO
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string Message { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public UserInfoDTO? User { get; set; }
    }
}
