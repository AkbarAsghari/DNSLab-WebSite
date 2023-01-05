using DNSLab.DTOs.IP;

namespace DNSLab.Shared;
partial class MainLayout
{
    private IPDTO IPDTO = new IPDTO();

    protected override void OnInitialized()
    {
        IPDTO iPDTO = new IPDTO();

        try
        {
            iPDTO.IPv4 = httpContextAccessor.HttpContext.Connection?.RemoteIpAddress.ToString();
        }
        catch (Exception ex)
        {
            //If Null reference exception (Please Enable WebSocket In Feature For IIS *Akbar*)
            iPDTO.IPv4 = "Unavaible";
        }
        IPDTO = iPDTO;
    }
}
