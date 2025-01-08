namespace DNSLab.Web.DTOs.Repositories.DDNS
{
    public class UpdateTokenAndRecordsDTO : UpdateTokenDTO
    {
        public List<Guid> Records { get; set; }
    }
}
