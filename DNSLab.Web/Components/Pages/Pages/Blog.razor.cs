using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Pages;

partial class Blog
{
    [Inject] IPageRepository _PageRepository { get; set; }

    [Parameter] public string? Url { get; set; }

    PageDTO? _Page;

    string HTML;
    protected override async Task OnInitializedAsync()
    {
        if (!String.IsNullOrEmpty(Url))
        {
            _Page = await _PageRepository.GetPageByUrl(Url);
        }
    }
}
