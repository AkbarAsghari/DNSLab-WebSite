namespace DNSLab.DTOs.DNSLookUp
{
    public class MXRecordDTO : RecordInfoDTO
    {
        public string Exchange { get; set; }
        public string Preference { get; set; }
    }
}
