using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Enums.DNSQueryLogs
{
    public enum ResponseTypeEnum
    {
        Authoritative,
        Recursive,
        Cached,
        Blocked,
        UpstreamBlocked,
        CacheBlocked
    }
}
