namespace DNSLab.DTOs.IP
{
    public class PingDTO
    {
        public int BufferSize { get; set; }
        public string IP { get; set; }
        public int TTL { get; set; }
        public long Time { get; set; }
        public bool Success { get; set; }
    }
}
