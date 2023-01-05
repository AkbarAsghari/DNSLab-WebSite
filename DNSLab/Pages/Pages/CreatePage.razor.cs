using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Pages;
partial class CreatePage
{
    private List<string> Tags;

    private PageDTO page { get; set; } = new PageDTO
    {
        URL = String.Empty,
        Title = String.Empty,
        Description = String.Empty,
        Keywords = new List<string>(),
        Body = "مطلب خود را در اینجا بنویسید.",
    };

    public async Task Create()
    {
        if (await _PageRepository.AddNewPage(page))
        {
            _navManager.NavigateTo("Page/MyPages");
            toastService.ShowToast(localizer["PageCreated"], Enums.ToastLevel.Success);
        }
    }
}
