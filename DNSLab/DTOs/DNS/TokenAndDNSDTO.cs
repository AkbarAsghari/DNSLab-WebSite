using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class TokenAndDNSDTO : TokenDTO
    {
        public ICollection<Guid> HostNameIds { get; set; }
    }
}
