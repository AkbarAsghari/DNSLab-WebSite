namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> Verify(long trackId);
        Task<string?> RequestPaymentUrl(int amount);
    }
}
