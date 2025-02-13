using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using PSC.Blazor.Components.MarkdownEditor;

namespace DNSLab.Web.Components.Pages.Pages;

partial class Page
{
    [Inject] IPageRepository _PageRepository { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }

    [Parameter] public Guid PageId { get; set; } = Guid.Empty;

    PageDTO _Page = new();

    MarkdownEditor _MarkdownEditor;

    protected override async Task OnInitializedAsync()
    {
        if (PageId != Guid.Empty)
        {
            var existPage = await _PageRepository.GetPage(PageId);
            if (existPage != null)
            {
                _Page = existPage;
                await _MarkdownEditor.SetValueAsync(_Page.Body);
                await InvokeAsync(() => StateHasChanged());
            }
        }
    }

    public async Task Submit()
    {
        if (PageId == Guid.Empty)
        {
            if (await _PageRepository.CreatePage(_Page))
            {
                _Snackbar.Add("صفحه اینجاد شد", Severity.Success);
            }
        }
        else
        {
            if (await _PageRepository.UpdatePage(_Page))
            {
                _Snackbar.Add("صفحه ویرایش شد", Severity.Success);
            }
        }
    }
}
