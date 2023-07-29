using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Dashboard
{
    public class DashboardDTO
    {
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public int OpenTicketsCount { get; set; }
        public int ActiveDNSCount { get; set; }
        public int IPChangeCountInLast24Hours { get; set; }
    }
}
