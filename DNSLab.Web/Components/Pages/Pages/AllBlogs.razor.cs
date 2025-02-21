using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Pages;

partial class AllBlogs
{
    [Inject] IPageRepository _PageRepository { get; set; }

    IEnumerable<PageInfoDTO>? _Pages { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _Pages = await _PageRepository.GetLastPages();
    }
}
