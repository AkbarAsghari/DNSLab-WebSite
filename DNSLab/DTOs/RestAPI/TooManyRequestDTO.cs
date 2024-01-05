namespace DNSLab.DTOs.RestAPI
{
    public class TooManyRequestDTO
    {
        public string Error { get; set; }
        public string MaximumAllowed { get; set; }
        public string Per { get; set; }
        public string Remain { get; set; }
    }
}
