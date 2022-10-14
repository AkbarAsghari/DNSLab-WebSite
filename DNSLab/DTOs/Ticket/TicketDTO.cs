using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Ticket
{
    public class TicketDTO
    {
        public Guid  ID { get; set; }
        public string Title { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
