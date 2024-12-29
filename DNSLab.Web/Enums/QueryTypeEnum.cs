using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.Enums
{
    public enum QueryTypeEnum
    {
        None = 0,
        A = 1,
        NS = 2,
        [Obsolete("Use MX")]
        MD = 3,
        [Obsolete("Use MX")]
        MF = 4,
        CNAME = 5,
        SOA = 6,
        MB = 7,
        MG = 8,
        MR = 9,
        NULL = 10,
        WKS = 11,
        PTR = 12,
        HINFO = 13,
        MINFO = 14,
        MX = 15,
        TXT = 16,
        RP = 17,
        AFSDB = 18,
        AAAA = 28,
        SRV = 33,
        NAPTR = 35,
        CERT = 37,
        DS = 43,
        RRSIG = 46,
        NSEC = 47,
        DNSKEY = 48,
        NSEC3 = 50,
        NSEC3PARAM = 51,
        TLSA = 52,
        SPF = 99,
        AXFR = 252,
        ANY = 255,
        URI = 256,
        CAA = 257,
        SSHFP = 44
    }
}
