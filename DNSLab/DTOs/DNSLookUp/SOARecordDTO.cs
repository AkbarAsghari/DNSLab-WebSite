namespace DNSLab.DTOs.DNSLookUp
{
    public class SOARecordDTO : RecordInfoDTO
    {
        public uint Expire { get; set; }
        public uint Minimum { get; set; }
        public string MName { get; set; }
        public uint Refresh { get; set; }
        public uint Retry { get; set; }
        public string RName { get; set; }
        public uint Serial { get; set; }
    }
}
