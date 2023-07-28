using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Tips
{
    public class TipsInformationDTO
    {
        public int Goal { get; set; }
        public int PrecentTowardsToCurrentMonthGoal { get; set; }

        public int CurrentPaidsCount { get; set; }
        public long CurrentPaidsAmount { get; set; }
        public IEnumerable<Guid> CurrentPaidsUsers { get; set; }

        public int PastPaidsCount { get; set; }
        public long PastPaidsAmount { get; set; }
        public IEnumerable<Guid> PastPaidsUsers { get; set; }
    }
}
