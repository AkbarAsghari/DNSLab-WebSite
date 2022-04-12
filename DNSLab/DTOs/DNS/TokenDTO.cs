using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class TokenDTO
    {
        public Guid Id { get; set; }
        public string? Key { get; set; }
        public string? Name { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
