using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs.Ticket;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Tickets;

partial class AllTickets
{
    [Inject] ITicketRepository _TicketRepository { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    IEnumerable<TicketDTO>? _Tickets { get; set; }

    bool _IsLoading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            _Tickets = await _TicketRepository.GetMyTickets();

            _IsLoading = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }

    async Task AddTicket()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Medium };

        var dialog = await _DialogService.ShowAsync<AddTicket>("اضافه کردن تیکت جدید", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnAfterRenderAsync(true);
        }
    }
}
