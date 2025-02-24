using DNSLab.Web.Enums;

namespace DNSLab.Web.DTOs.Repositories.Payment
{
    public class PaymentDTO
    {
        public long Amount { get; set; }
        public string? Description { get; set; }
        public PaymentStatusEnum StatusId { get; set; }
        public long? RefNumber { get; set; }
        public DateTime? PaidAt { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
