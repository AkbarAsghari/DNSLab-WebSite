using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools;
partial class Ping
{
    private HostOrIPAddressDTO hostOrIPAddress = new HostOrIPAddressDTO();
    private bool isProgressing = false;
    private string result = String.Empty;

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    public async Task OnValidSubmit()
    {
        isProgressing = true;

        result = String.Empty;

        List<PingDTO> pings = new List<PingDTO>();

        int attemptTimes = 4;
        for (int i = 1; i <= attemptTimes; i++)
        {
            var ping = await iPRepository.Ping(hostOrIPAddress.HostOrIPAddress);

            if (ping == null)
            {
                isProgressing = false;
                return;
            }

            if (!ping.Success)
                result += $"error<br>";
            else
                result += $"{ping.BufferSize} bytes from {ping.IP}: icmp_seq={i} ttl={ping.TTL} time={ping.Time} ms<br>";

            await this.InvokeAsync(() => StateHasChanged());
            pings.Add(ping!);
            await Task.Delay(750);
        }

        int successCount = pings.Where(x => x.Success).Count();
        int unsuccessCount = attemptTimes - successCount;

        result += $"<br><br>--- {hostOrIPAddress.HostOrIPAddress} statics ---<br>";
        result += $"packets transmitted : {attemptTimes}<br>";
        result += $"recived : {successCount}<br>";
        result += $"packet loss : {(successCount == 0 ? 100 : unsuccessCount * 100 / successCount)}%<br>";

        if (pings.Any(x => x.Time > 0))
        {
            var avgRTT = pings.Where(x => x.Time > 0).Average(x => x.Time);
            result += "<br><br>--- Round Trip Time (rtt) ---<br>";
            result += $"min {pings.Where(x => x.Time > 0).Min(x => x.Time)} ms<br>";
            result += $"avg {avgRTT} ms<br>";
            result += $"max {pings.Where(x => x.Time > 0).Max(x => x.Time)} ms<br>";
        }

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(hostOrIPAddress.HostOrIPAddress))
            hostOrIPAddress.HostOrIPAddress = IPDTO.IPv4;
    }
}
