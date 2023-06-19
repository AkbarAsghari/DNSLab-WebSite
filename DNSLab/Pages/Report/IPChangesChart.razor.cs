using MudBlazor;
using System.Drawing;

namespace DNSLab.Pages.Report;
partial class IPChangesChart
{
    public List<ChartSeries> Series;
    public string[]? XAxisLabels;


    protected override async Task OnInitializedAsync()
    {
        await InitlineChartExample();
    }

    private async Task InitlineChartExample()
    {
        var last30DayIPChangesCount = await _DNSRepository.GetLast30DayIPChangesCount();

        XAxisLabels = new string[31];
        Series = new List<ChartSeries>();

        for (int i = -30; i <= 0; i++)
        {
            var date = DateTime.UtcNow.AddDays(i).Date;
            foreach (var item in last30DayIPChangesCount)
            {
                if (!item.DateAndCount.Any(x => x.Key == date))
                    item.DateAndCount.Add(date, 0);
            }

            XAxisLabels[30 + i] = date.ToLocalizerString().Substring(8, 2);
        }
        var haveChangeRecords = last30DayIPChangesCount.Where(x => x.DateAndCount.Any(x => x.Value > 0));
        for (int i = 0; i < haveChangeRecords.Count(); i++)
        {
            Series.Add(new ChartSeries
            {
                Name = haveChangeRecords.ElementAt(i).HostName,
                Data = haveChangeRecords.ElementAt(i).DateAndCount.OrderBy(x => x.Key).Select(x => Convert.ToDouble(x.Value)).ToArray()
            });
        }
    }
}