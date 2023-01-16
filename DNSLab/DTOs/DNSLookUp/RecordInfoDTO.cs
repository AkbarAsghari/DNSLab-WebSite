namespace DNSLab.DTOs.DNSLookUp
{
    public class RecordInfoDTO
    {
        public string RecordType { get; set; }
        public string DomainName { get; set; }
        public int TTL { get; set; }
    }
}
