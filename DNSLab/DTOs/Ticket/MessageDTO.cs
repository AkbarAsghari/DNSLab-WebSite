using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Ticket
{
    public class MessageDTO
    {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
