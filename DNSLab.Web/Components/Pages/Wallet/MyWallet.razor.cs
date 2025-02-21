using ApexCharts;
using DNSLab.Web.Components.Dialogs.Wallet;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Wallet;

partial class MyWallet
{
    [Inject] IWalletRepository _WalletRepository { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    WalletDTO? _Wallet { get; set; }
    IEnumerable<WalletTransactionDTO>? _WalletTransactions { get; set; }

    IEnumerable<Tuple<DateTime, int>>? _Last30DaysTransactionsChartDataChartData;

    bool _IsLoading = false;

    protected override async Task OnInitializedAsync()
    {
        _IsLoading = true;
        _Wallet = await _WalletRepository.GetWallet();
        _WalletTransactions = await _WalletRepository.GetWalletTransactions(0, 5);
        _Last30DaysTransactionsChartDataChartData = await _WalletRepository.GetLast30DaysTransactionsChartData();
        _IsLoading = false;
    }

    async Task IncreaseBalance()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<IncreaseBalanceDialog>("افزایش موجودی", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnInitializedAsync();
        }
    }

    ApexChartOptions<Tuple<DateTime, int>> _Last30DaysTransactionsChartDataChartDataOptions = new ApexChartOptions<Tuple<DateTime, int>>
    {
        Fill = new Fill { Opacity = 0 },
        Theme = new Theme { Mode = Mode.Dark, },
        Xaxis = new XAxis { AxisBorder = new AxisBorder { Show = false, }, Labels = new XAxisLabels { Show = false }, AxisTicks = new AxisTicks { Show = false } },
        Yaxis = new List<YAxis> { new YAxis { Show = false } },
        Grid = new Grid { Show = false },
        Chart = new Chart { Background = "transparent", Toolbar = new Toolbar { Tools = new ApexCharts.Tools { Zoomin = false, Zoomout = false, Download = false, Reset = false, Pan = false, Zoom = false } } },
    };
}
