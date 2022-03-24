using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class CreateHostNameDTO
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string IPv4Address { get; set; }
        public string IPv6Address { get; set; }
        public string HostNameAlias { get; set; }
        public int RecordType { get; set; }
    }
}
