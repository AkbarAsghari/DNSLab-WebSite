using DNSLab.DTOs.Ticket;
using MudBlazor;

namespace DNSLab.Pages.Tickets;
partial class MyTickets
{
    [Inject] private IDialogService DialogService { get; set; }

    IEnumerable<TicketDTO> tickets;


    protected override async Task OnInitializedAsync()
    {
        tickets = await ticketRepository.GetTickets();
    }

    async Task DeleteTicket(TicketDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
           "هشدار",
           $"آیا از حذف کامنت {record.Title} مطمئن هستید؟",
           yesText: "حذف", cancelText: "انصراف");

        if (result == true)
        {
            if (await ticketRepository.RemoveTicket(record.ID))
            {
                await OnInitializedAsync();
            }
        }
    }

    async Task CloseTicket(TicketDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
           "هشدار",
           $"آیا از بست کامنت {record.Title} مطمئن هستید؟",
           yesText: "بستن", cancelText: "انصراف");

        if (result == true)
        {
            if (await ticketRepository.CloseTicket(record.ID))
            {
                await OnInitializedAsync();
            }
        }
    }

    void OpenTicket(TicketDTO record)
    {
        navigationManager.NavigateTo("Ticket/Details/" + record.ID);
    }
}
