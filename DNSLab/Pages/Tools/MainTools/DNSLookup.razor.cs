using DNSLab.DTOs.DNSLookUp;
using DNSLab.DTOs.IP;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Net;

namespace DNSLab.Pages.Tools.MainTools;
partial class DNSLookup
{
    private HostOrIPAddressDTO hostOrIPAddress = new HostOrIPAddressDTO();
    private bool isProgressing = false;
    private string result { get; set; }

    string queryType { get; set; } = "A";

    private List<BitDropDownItem> DNSQueryTypes = new()
    {
        new BitDropDownItem()
        {
            Text = "A",
            Value = "A",
        },
        new BitDropDownItem()
        {
            Text = "NS",
            Value = "NS",
        },
        new BitDropDownItem()
        {
            Text = "CNAME",
            Value = "CNAME",
        },
        new BitDropDownItem()
        {
            Text = "SOA",
            Value = "SOA",
        },
        new BitDropDownItem()
        {
            Text = "MX",
            Value = "MX",
        },
        new BitDropDownItem()
        {
            Text = "TXT",
            Value = "TXT",
        }
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
        result = String.Empty;

        Navigation.NavigateTo($"tools/dnslookup?type={queryType}&host={hostOrIPAddress.HostOrIPAddress}");

        result += "<div class='table-responsive'>";
        result += "<table class='table'>";

        switch (queryType.ToUpper())
        {
            case "A":
                result += "<thead><tr>" +
                    "<th>RecordType</th>" +
                    "<th>DomainName</th>" +
                    "<th>TTL</th>" +
                    "<th>Address</th>" +
                "</tr></thead>";
                result += "<tbody>";
                var aResponse = await _DNSLookUpRepository.Query<ARecordDTO>(hostOrIPAddress.HostOrIPAddress);
                foreach (var item in aResponse)
                    result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.Address}</td></tr>";
                result += "</tbody>";
                break;
            case "NS":
                result += "<thead><tr>" +
                    "<th>RecordType</th>" +
                    "<th>DomainName</th>" +
                    "<th>TTL</th>" +
                    "<th>NSDName</th>" +
                "</tr></thead>";
                result += "<tbody>";
                var nsResponse = await _DNSLookUpRepository.Query<NsRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                foreach (var item in nsResponse)
                    result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.NSDName}</td></tr>";
                result += "</tbody>";
                break;
            case "CNAME":
                result += "<thead><tr>" +
                   "<th>RecordType</th>" +
                   "<th>DomainName</th>" +
                   "<th>TTL</th>" +
                   "<th>CanonicalName</th>" +
               "</tr></thead>";
                result += "<tbody>";
                var cnameResponse = await _DNSLookUpRepository.Query<CNAMERecordDTO>(hostOrIPAddress.HostOrIPAddress);
                foreach (var item in cnameResponse)
                    result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.CanonicalName}</td></tr>";
                result += "</tbody>";
                break;
            case "MX":
                result += "<thead><tr>" +
                  "<th>RecordType</th>" +
                  "<th>DomainName</th>" +
                  "<th>TTL</th>" +
                  "<th>Exchange</th>" +
                  "<th>Preference</th>" +
              "</tr></thead>";
                result += "<tbody>";
                var mxResponse = await _DNSLookUpRepository.Query<MXRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                foreach (var item in mxResponse)
                    result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.Exchange}</td><td>{item.Preference}</td></tr>";
                result += "</tbody>";
                break;
            case "TXT":
                result += "<thead><tr>" +
                 "<th>RecordType</th>" +
                 "<th>DomainName</th>" +
                 "<th>TTL</th>" +
                 "<th>EscapedText</th>" +
                 "<th>Text</th>" +
             "</tr></thead>";
                result += "<tbody>";
                var txtResponse = await _DNSLookUpRepository.Query<TXTRecordDTO>(hostOrIPAddress.HostOrIPAddress);
                foreach (var item in txtResponse)
                    result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.EscapedText}</td><td>{item.Text}</td></tr>";
                result += "</tbody>";
                break;
            case "SOA":
                result += "<thead><tr>" +
                 "<th>RecordType</th>" +
                 "<th>DomainName</th>" +
                 "<th>TTL</th>" +
                 "<th>Expire</th>" +
                 "<th>Minimum</th>" +
                 "<th>MName</th>" +
                 "<th>Refresh</th>" +
                 "<th>Retry</th>" +
                 "<th>RName</th>" +
                 "<th>Serial</th>" +
             "</tr></thead>";
                result += "<tbody>";
                var soaResponse = await _DNSLookUpRepository.Query<SOARecordDTO>(hostOrIPAddress.HostOrIPAddress);
                foreach (var item in soaResponse)
                    result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.Expire}</td><td>{item.Minimum}</td><td>{item.MName}</td><td>{item.Refresh}</td><td>{item.Retry}</td><td>{item.RName}</td><td>{item.Serial}</td></tr>";
                result += "</tbody>";
                break;
            default:
                break;
        }

        result += "</table>";
        result += "</div>";

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = "dnslab.link";
    }
}
