
using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.DynamicDNSs;

partial class MikroTikSettings
{
    [Inject] IDDNSRepository _DDNSRepository { get; set; }
    IEnumerable<UpdateTokenDTO>? _Tokens { get; set; }

    bool _IsLoading = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            _Tokens = await _DDNSRepository.GetTokens();

            _IsLoading = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }


    UpdateTokenDTO? _Token { get; set; } = null;
    void SelectToken(UpdateTokenDTO token)
    {
        _Token = token;
    }
}
