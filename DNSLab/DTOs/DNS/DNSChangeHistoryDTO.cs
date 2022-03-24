namespace DNSLab.DTOs.DNS
{
    public class SameHistoryDTO
    {
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class DNSChangeHistoryDTO : SameHistoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int RecordType { get; set; }
        public List<ARecordHistoryDTO> ARecordHistories { get; set; }
        public List<AAAARecordHistoryDTO> AAAARecordHistories { get; set; }
        public List<CNameRecordHistoryDTO> CNameRecordHistories { get; set; }
    }

    public class ARecordHistoryDTO : SameHistoryDTO
    {
        public string IPv4Address { get; set; }
    }

    public class AAAARecordHistoryDTO : SameHistoryDTO
    {
        public string IPv6Address { get; set; }
    }

    public class CNameRecordHistoryDTO : SameHistoryDTO
    {
        public string HostNameAlias { get; set; }
    }
}
