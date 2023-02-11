using DNSLab.DTOs.IP;

namespace DNSLab.Pages;
partial class Index
{
    Guid PageId = new Guid("00000000-0000-0000-0000-000000000001");
    [CascadingParameter] public IPDTO IPDTO { get; set; }
}
