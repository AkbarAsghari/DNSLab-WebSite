
using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class All
{
    [Inject] IAccountRepository _AccountRepository { get; set; }


    IEnumerable<UserDTO>? _AllUsers { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _AllUsers = await _AccountRepository.GetAllAsync();
    }
}
