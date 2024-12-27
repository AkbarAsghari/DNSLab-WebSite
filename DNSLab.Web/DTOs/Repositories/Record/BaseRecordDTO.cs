using DNSLab.Web.Enums;

namespace DNSLab.Web.DTOs.Repositories.Record
{
    public class BaseRecordDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Disable { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime LastModified { get; set; }
        public uint TTL { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public RecordTypeEnum Type { get; set; }
        public object RData { get; set; }
    }
}
