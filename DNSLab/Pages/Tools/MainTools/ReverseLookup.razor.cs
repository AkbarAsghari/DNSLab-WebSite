using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools.MainTools;
partial class ReverseLookup
{
    private IPDTO iP = new IPDTO();
    private bool isProgressing = false;
    private List<string> result { get; set; } = new List<string>();

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    [Parameter, SupplyParameterFromQuery]
    public string? ip { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!String.IsNullOrEmpty(ip))
        {
            iP.IPv4 = ip;
            await OnValidSubmit();
        }
    }

    public async Task OnValidSubmit()
    {
        if (isProgressing) return;

        isProgressing = true;

        result.Clear();

        if (ip != iP.IPv4)
            Navigation.NavigateTo($"tools/reverselookup?ip={iP.IPv4}");

        var response = await _DNSLookUpRepository.QueryReverse(iP.IPv4);
        if (!String.IsNullOrEmpty(response))
            result.Add(response);

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(iP.IPv4))
            iP.IPv4 = IPDTO.IPv4;
    }
}
