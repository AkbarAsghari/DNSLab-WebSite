using DNSLab.DTOs.DNS;
using DNSLab.DTOs.IP;

namespace DNSLab.Pages.DNS;
partial class DNSForm
{
    [Parameter] public HostNameDTO HostName { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public bool IsNewRecord { get; set; } = false;
    [CascadingParameter] public IPDTO IPDTO { get; set; }

    public class RecordType
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public List<RecordType> RecordTypes = new List<RecordType>
    {
        new RecordType {Text= "DNS Host (A)" ,Value = "1"},
        new RecordType {Text= "AAAA (IPv6)" ,Value = "2"},
        new RecordType {Text= "DNS Alias (CNAME)" ,Value = "3"},
        new RecordType {Text= "Web Redirect" ,Value = "4"},
    };

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(HostName.IPv4Address))
            HostName.IPv4Address = IPDTO.IPv4;
    }
}
