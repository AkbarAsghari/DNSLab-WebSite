using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Zone
{
    public class ZoneDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Disable { get; set; }
    }
}
