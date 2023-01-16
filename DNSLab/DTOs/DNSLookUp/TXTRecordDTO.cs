namespace DNSLab.DTOs.DNSLookUp
{
    public class TXTRecordDTO : RecordInfoDTO
    {
        public string EscapedText { get; set; }
        public string Text { get; set; }
    }
}
