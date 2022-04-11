using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class TokenDTO
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<HostNameDTO> HostNames { get; set; }
    }
}
