using DNSLab.DTOs.DNSLookUp;

namespace DNSLab.Interfaces.Repository
{
    public interface IDNSLookUpRepository
    {
        public Task<IEnumerable<T>> Query<T>(string query) where T : RecordInfoDTO;
        public Task<string> QueryReverse(string iPAddress);
    }
}
