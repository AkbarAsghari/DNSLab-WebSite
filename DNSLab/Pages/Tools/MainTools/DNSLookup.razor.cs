using DNSLab.DTOs.DNSLookUp;
using DNSLab.DTOs.IP;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Net;

namespace DNSLab.Pages.Tools.MainTools;
partial class DNSLookup
{
    private HostOrIPAddressDTO hostOrIPAddress = new HostOrIPAddressDTO();
    private bool isProgressing = false;
    private List<string> result { get; set; } = new List<string>();

    string queryType { get; set; } = "A";

    private List<string> DNSQueryTypes = new()
    {
        "A","AAAA","NS","CNAME","SOA","MX","TXT",
    };

    [Parameter, SupplyParameterFromQuery]
    public string? type { get; set; }
    [Parameter, SupplyParameterFromQuery]
    public string? host { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!String.IsNullOrEmpty(type))
        {
            queryType = type.ToUpper();
        }
        if (!String.IsNullOrEmpty(host))
        {
            hostOrIPAddress.HostOrIPAddress = host;
        }
        if (!String.IsNullOrEmpty(host) && !String.IsNullOrEmpty(type))
        {
            await OnValidSubmit();
        }
    }

    public async Task OnValidSubmit()
    {
        if (isProgressing) return;

        isProgressing = true;

        result.Clear();

        if (type != queryType || host != hostOrIPAddress.HostOrIPAddress)
            Navigation.NavigateTo($"tools/dnslookup?type={queryType}&host={hostOrIPAddress.HostOrIPAddress}",options: new NavigationOptions{
                ReplaceHistoryEntry = true
            });

        switch (queryType.ToUpper())
        {
            case "A":
                {
                    var response = await _DNSLookUpRepository.Query<ARecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"Address : {item.Address}");
                }
                break;
            case "AAAA":
                {
                    var response = await _DNSLookUpRepository.Query<AaaaRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"Address : {item.Address}");
                }
                break;
            case "NS":
                {
                    var response = await _DNSLookUpRepository.Query<NsRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"NSDName : {item.NSDName}");
                }
                break;
            case "CNAME":
                {
                    var response = await _DNSLookUpRepository.Query<CNAMERecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"CanonicalName : {item.CanonicalName}");
                }
                break;
            case "MX":
                {
                    var response = await _DNSLookUpRepository.Query<MXRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"Exchange : {item.Exchange}<br>" +
                            $"Preference : {item.Preference}");
                }
                break;
            case "TXT":
                {
                    var response = await _DNSLookUpRepository.Query<TXTRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"EscapedText : {item.EscapedText}<br>" +
                            $"Text : {item.Text}");
                }
                break;
            case "SOA":
                {
                    var response = await _DNSLookUpRepository.Query<SOARecordDTO>(hostOrIPAddress.HostOrIPAddress);
                    foreach (var item in response)
                        result.Add($"RecordType : {item.RecordType}<br>" +
                            $"DomainName : {item.DomainName}<br>" +
                            $"TTL : {item.TTL}<br>" +
                            $"Expire : {item.Expire}<br>" +
                            $"Minimum : {item.Minimum}<br>" +
                            $"MName : {item.MName}<br>" +
                            $"Refresh : {item.Refresh}<br>" +
                            $"Retry : {item.Retry}<br>" +
                            $"RName : {item.RName}<br>" +
                            $"Serial : {item.Serial}");
                }
                break;
            default:
                break;
        }

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = "dnslab.link";
    }
}
