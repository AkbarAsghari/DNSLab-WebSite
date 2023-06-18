using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;
using MudBlazor;

namespace DNSLab.Pages.DNS;
partial class MyDNS
{
    [Inject] private IDialogService DialogService { get; set; }

    List<HostNameDTO> hostNames;

    PaginationDTO paginationDTO = new PaginationDTO() { RecordsPerPage = 5 };

    private int totalAmountPages { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadHostNames();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task LoadHostNames()
    {
        var paginatedResponse = await dnsRepository.GetOwnHosts(paginationDTO);
        hostNames = paginatedResponse.Items.ToList();
        totalAmountPages = paginatedResponse.TotalAmountPages;
    }

    private async Task SelectedPage(int page)
    {
        paginationDTO.Page = page;
        await LoadHostNames();
    }

    private async Task DeleteHostNameAsync(HostNameDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
            "هشدار",
            $"آیا از حذف رکورد {record.Name} مطمئن هستید؟",
            yesText: "حذف", cancelText: "انصراف");

        if (result == true)
        {
            if (await dnsRepository.DeleteHostName(record.ID))
            {
                await LoadHostNames();
            }
        }
    }

    private async Task EditHostName(HostNameDTO record)
    {
        NavigationManager.NavigateTo("dns/edit/" + record.ID);
    }
}
