using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.Subscriptions
{
    public class PlanInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Creadit { get; set; }
        public int PricePerMonth { get; set; }
        public int TotalPrice { get; set; }
    }
}
