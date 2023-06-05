using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools.MainTools;
partial class Ping
{
    private HostOrIPAddressDTO hostOrIPAddress = new HostOrIPAddressDTO();
    private bool isProgressing = false;
    private string result = String.Empty;

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    [Parameter, SupplyParameterFromQuery]
    public string? host { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (!String.IsNullOrEmpty(host))
        {
            hostOrIPAddress.HostOrIPAddress = host;
            await OnValidSubmit();
        }
    }

    public async Task OnValidSubmit()
    {
        if (isProgressing) return;

        isProgressing = true;

        host = hostOrIPAddress.HostOrIPAddress;

        Navigation.NavigateTo($"tools/ping?host={hostOrIPAddress.HostOrIPAddress}");

        result = String.Empty;

        List<PingDTO> pings = new List<PingDTO>();

        int attemptTimes = 4;
        for (int i = 1; i <= attemptTimes; i++)
        {
            PingDTO ping = await iPRepository.Ping(hostOrIPAddress.HostOrIPAddress);

            if (ping == null)
            {
                isProgressing = false;
                return;
            }

            if (!ping.Success)
                result += $"<b style='color: red;'>error</b><br>";
            else
                result += $"{ping.BufferSize} bytes from {ping.IP}: icmp_seq={i} ttl={ping.TTL} time={ping.Time} ms<br>";

            await this.InvokeAsync(() => StateHasChanged());
            pings.Add(ping!);
            await Task.Delay(750);
        }

        IEnumerable<PingDTO> successPings = pings.Where(x => x.Success);

        int successCount = successPings.Count();
        int unsuccessCount = attemptTimes - successCount;

        result += $"<br><br>--- {hostOrIPAddress.HostOrIPAddress} statics ---<br>";
        result += $"packets transmitted : {attemptTimes}<br>";
        result += $"recived : {successCount}<br>";
        result += $"packet loss : {(successCount == 0 ? 100 : unsuccessCount * 100 / successCount)}%<br>";

        if (successCount > 0)
        {
            var avgRTT = successPings.Average(x => x.Time);
            result += "<br><br>--- Round Trip Time (rtt) ---<br>";
            result += $"min {successPings.Min(x => x.Time)} ms<br>";
            result += $"avg {avgRTT} ms<br>";
            result += $"max {successPings.Max(x => x.Time)} ms<br>";
        }

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = IPDTO.IPv4;
    }
}
