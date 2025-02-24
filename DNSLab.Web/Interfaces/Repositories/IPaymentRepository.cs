using DNSLab.Web.DTOs.Repositories.Payment;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> Verify(long trackId);
        Task<string?> RequestPaymentUrl(int amount);
        Task<IEnumerable<PaymentDTO>?> GetPayments(int startIndex, int count);
        Task<IEnumerable<PaymentDTO>?> GetAllPayments(int startIndex, int count);
        Task<int?> GetPaymentsCount();
        Task<int?> GetAllPaymentsCount();
    }
}
