namespace DNSLab.Web.DTOs.Repositories.DDNS
{
    public class UpdateTokenDTO
    {
        public Guid Id { get; set; }
        public string? Key { get; set; }
        public string Name { get; set; }
    }
}
