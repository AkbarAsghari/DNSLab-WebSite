using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;

namespace DNSLab.Pages.DNS;
partial class MyDNS
{
    List<HostNameDTO> hostNames;

    PaginationDTO paginationDTO = new PaginationDTO() { RecordsPerPage = 5 };

    bool deleteModalVisible;
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
        var paginatedResponse = await dnsRepository.GetOwnHosts(paginationDTO);
        hostNames = paginatedResponse.Items.ToList();
        totalAmountPages = paginatedResponse.TotalAmountPages;
    }

    private async Task SelectedPage(int page)
    {
        paginationDTO.Page = page;
        await LoadHostNames();
    }

    private async Task AcceptDelete()
    {
        if (deleteRcord != null)
        {
            if (await dnsRepository.DeleteHostName(deleteRcord.ID))
            {
                deleteModalVisible = false;
                await LoadHostNames();
            }
        }
        deleteRcord = new HostNameDTO();
    }

    private void DeleteHostName(HostNameDTO record)
    {
        deleteRcord = record;
        deleteModalVisible = true;
    }

    private async Task EditHostName(HostNameDTO record)
    {
        NavigationManager.NavigateTo("dns/edit/" + record.ID);
    }
}
