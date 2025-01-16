namespace DNSLab.Web.DTOs.Repositories.Wallet
{
    public class WalletTransactionDTO
    {
        public long Amount { get; set; }
        public string Description { get; set; }
        public bool Incremental { get; set; }
        public string TransactionType { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
