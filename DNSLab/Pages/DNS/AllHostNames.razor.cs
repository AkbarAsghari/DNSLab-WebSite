using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;

namespace DNSLab.Pages.DNS;
partial class AllHostNames
{
    List<HostNameAndUserDTO> hostNames;

    PaginationDTO paginationDTO = new PaginationDTO() { RecordsPerPage = 25 };

    Modal DeleteModal { get; set; }
    HostNameDTO deleteRcord { get; set; } = new HostNameDTO();

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
        var paginatedResponse = await dnsRepository.GetHostNames(paginationDTO);
        hostNames = paginatedResponse.Items.ToList();
        totalAmountPages = paginatedResponse.TotalAmountPages;
    }

    private async Task SelectedPage(int page)
    {
        paginationDTO.Page = page;
        await LoadHostNames();
    }
}
