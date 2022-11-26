using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Transactions
{
    public class TipTransactionDTO
    {
        public Guid? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public int Amount { get; set; }
        public string? OrderId { get; set; }
    }
}
