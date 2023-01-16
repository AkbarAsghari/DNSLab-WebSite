namespace DNSLab.DTOs.DNSLookUp
{
    public class MXRecordDTO : RecordInfoDTO
    {
        public string Exchange { get; set; }
        public ushort Preference { get; set; }
    }
}
