using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class CreateTokenDTO
    {
        public string TokenName { get; set; }
        public IEnumerable<Guid> DomainNameSystemsIds { get; set; }
    }
}
