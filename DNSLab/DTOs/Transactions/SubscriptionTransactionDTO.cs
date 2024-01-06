namespace DNSLab.DTOs.Transactions
{
    public class SubscriptionTransactionDTO
    {
        public Guid Id { get; set; }
        public string OwnerFullName { get; set; }
        public string PlanName { get; set; }
        public long TrackId { get; set; }
        public long Amount { get; set; }
        public string? Description { get; set; }
        public string? OrderId { get; set; }
        public int TransactionStatusId { get; set; }
        public long? RefNumber { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? CardNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
