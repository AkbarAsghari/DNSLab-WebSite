using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class QueryLogsResponseDTO
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public int pageNumber { get; set; }
        public int totalPages { get; set; }
        public int totalEntries { get; set; }
        public Entry[] entries { get; set; }
    }

    public class Entry
    {
        public int rowNumber { get; set; }
        public DateTime timestamp { get; set; }
        public string clientIpAddress { get; set; }
        public string protocol { get; set; }
        public string responseType { get; set; }
        public string rcode { get; set; }
        public string qname { get; set; }
        public string qtype { get; set; }
        public string qclass { get; set; }
        public string answer { get; set; }
    }

}
