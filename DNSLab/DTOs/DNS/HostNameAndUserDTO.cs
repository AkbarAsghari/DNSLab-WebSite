using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class HostNameAndUserDTO : HostNameDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }
}
