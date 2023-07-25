using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.DTOs.DNS
{
    public class CreateHostNameDTO
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        string _IPv4Address;
        public string IPv4Address
        {
            get
            {
                return _IPv4Address;
            }
            set
            {
                _IPv4Address = value.PersianToEnglishNumbers();
            }
        }
        string _IPv6Address;
        public string IPv6Address
        {
            get
            {
                return _IPv6Address;
            }
            set
            {
                _IPv6Address = value.PersianToEnglishNumbers();
            }
        }
        string _HostNameAlias;
        public string HostNameAlias
        {
            get
            {
                return _HostNameAlias;
            }
            set
            {
                _HostNameAlias = value.PersianToEnglishNumbers();
            }
        }
        string _URLOrIp;
        public string URLOrIp
        {
            get
            {
                return _URLOrIp;
            }
            set
            {
                _URLOrIp = value.PersianToEnglishNumbers();
            }
        }
        public int? RedirectHttpResponseStatusCode { get; set; }
        public int RecordType { get; set; }
        public int Ttl { get; set; } = 60;
    }
}
