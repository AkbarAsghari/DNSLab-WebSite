using DNSLab.DTOs.DNSLookUp;
using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools;
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

    public async Task OnValidSubmit()
    {
        isProgressing = true;
        result = String.Empty;

        result += "<div class='table-responsive-sm'>";
        result += "<table class='table'>";

        if (queryType == "A")
        {
            result += "<thead><tr>" +
                    "<th scope='col'>RecordType</th>" +
                    "<th scope='col'>DomainName</th>" +
                    "<th scope='col'>TTL</th>" +
                    "<th scope='col'>Address</th>" +
                "</tr></thead>";
            result += "<tbody>";
            var response = await _DNSLookUpRepository.Query<ARecordDTO>(hostOrIPAddress.HostOrIPAddress);
            foreach (var item in response)
                result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.Address}</td></tr>";
            result += "</tbody>";
        }
        else if (queryType == "NS")
        {
            result += "<thead><tr>" +
                    "<th scope='col'>RecordType</th>" +
                    "<th scope='col'>DomainName</th>" +
                    "<th scope='col'>TTL</th>" +
                    "<th scope='col'>NSDName</th>" +
                "</tr></thead>";
            result += "<tbody>";
            var response = await _DNSLookUpRepository.Query<NsRecordDTO>(hostOrIPAddress.HostOrIPAddress);
            foreach (var item in response)
                result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.NSDName}</td></tr>";
            result += "</tbody>";
        }
        else if (queryType == "CNAME")
        {
            result += "<thead><tr>" +
                   "<th scope='col'>RecordType</th>" +
                   "<th scope='col'>DomainName</th>" +
                   "<th scope='col'>TTL</th>" +
                   "<th scope='col'>CanonicalName</th>" +
               "</tr></thead>";
            result += "<tbody>";
            var response = await _DNSLookUpRepository.Query<CNAMERecordDTO>(hostOrIPAddress.HostOrIPAddress);
            foreach (var item in response)
                result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.CanonicalName}</td></tr>";
            result += "</tbody>";
        }
        else if (queryType == "MX")
        {
            result += "<thead><tr>" +
                   "<th scope='col'>RecordType</th>" +
                   "<th scope='col'>DomainName</th>" +
                   "<th scope='col'>TTL</th>" +
                   "<th scope='col'>Exchange</th>" +
                   "<th scope='col'>Preference</th>" +
               "</tr></thead>";
            result += "<tbody>";
            var response = await _DNSLookUpRepository.Query<MXRecordDTO>(hostOrIPAddress.HostOrIPAddress);
            foreach (var item in response)
                result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.Exchange}</td><td>{item.Preference}</td></tr>";
            result += "</tbody>";
        }
        else if (queryType == "TXT")
        {
            result += "<thead><tr>" +
                  "<th scope='col'>RecordType</th>" +
                  "<th scope='col'>DomainName</th>" +
                  "<th scope='col'>TTL</th>" +
                  "<th scope='col'>EscapedText</th>" +
                  "<th scope='col'>Text</th>" +
              "</tr></thead>";
            result += "<tbody>";
            var response = await _DNSLookUpRepository.Query<TXTRecordDTO>(hostOrIPAddress.HostOrIPAddress);
            foreach (var item in response)
                result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.EscapedText}</td><td>{item.Text}</td></tr>";
            result += "</tbody>";
        }
        else if (queryType == "SOA")
        {
            result += "<thead><tr>" +
                 "<th scope='col'>RecordType</th>" +
                 "<th scope='col'>DomainName</th>" +
                 "<th scope='col'>TTL</th>" +
                 "<th scope='col'>Expire</th>" +
                 "<th scope='col'>Minimum</th>" +
                 "<th scope='col'>MName</th>" +
                 "<th scope='col'>Refresh</th>" +
                 "<th scope='col'>Retry</th>" +
                 "<th scope='col'>RName</th>" +
                 "<th scope='col'>Serial</th>" +
             "</tr></thead>";
            result += "<tbody>";
            var response = await _DNSLookUpRepository.Query<SOARecordDTO>(hostOrIPAddress.HostOrIPAddress);
            foreach (var item in response)
                result += $"<tr><td>{item.RecordType}</td><td>{item.DomainName}</td><td>{item.TTL}</td><td>{item.Expire}</td><td>{item.Minimum}</td><td>{item.MName}</td><td>{item.Refresh}</td><td>{item.Retry}</td><td>{item.RName}</td><td>{item.Serial}</td></tr>";
            result += "</tbody>";
        }

        result += "</table>";
        result += "</div>";

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = "dnslab.ir";
    }
}
