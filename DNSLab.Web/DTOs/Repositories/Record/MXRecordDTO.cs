namespace DNSLab.Web.DTOs.Repositories.Record
{
    public class MXRecordDTO : BaseRecordDataDTO
    {
        public string Exchange { get; set; }
        public ushort Preference { get; set; }
    }
}
