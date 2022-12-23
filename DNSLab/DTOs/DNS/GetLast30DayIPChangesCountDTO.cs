namespace DNSLab.DTOs.DNS
{
    public class GetLast30DayIPChangesCountDTO
    {
        public string HostName { get; set; }
        public Dictionary<DateTime, int> DateAndCount { get; set; }
    }
}
