using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class QueryLogsResponseDTO
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalEntries { get; set; }
        public Entry[] Entries { get; set; }
    }

    public class Entry
    {
        public int RowNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string ClientIpAddress { get; set; }
        public string Protocol { get; set; }
        public string ResponseType { get; set; }
        public string Rcode { get; set; }
        public string Qname { get; set; }
        public string Qtype { get; set; }
        public string Qclass { get; set; }
        public string Answer { get; set; }
    }

}
