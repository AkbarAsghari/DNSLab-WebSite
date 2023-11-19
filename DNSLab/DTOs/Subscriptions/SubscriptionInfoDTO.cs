using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Subscriptions
{
    public class SubscriptionInfoDTO
    {
        public PlanInfoDTO Plan { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TransactionStatus { get; set; }
    }
}
