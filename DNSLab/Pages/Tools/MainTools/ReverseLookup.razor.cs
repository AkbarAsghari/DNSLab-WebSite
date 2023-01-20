using DNSLab.DTOs.IP;

namespace DNSLab.Pages.Tools.MainTools;
partial class ReverseLookup
{
    private IPDTO iP = new IPDTO();
    private bool isProgressing = false;
    private string result { get; set; }

    [CascadingParameter] public IPDTO IPDTO { get; set; }

    public async Task OnValidSubmit()
    {
        isProgressing = true;

        var response = await _DNSLookUpRepository.QueryReverse(iP.IPv4);
        if (!String.IsNullOrEmpty(response))
            result = response;
        else
            result = String.Empty;

        isProgressing = false;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (String.IsNullOrEmpty(iP.IPv4))
            iP.IPv4 = IPDTO.IPv4;
    }
}
