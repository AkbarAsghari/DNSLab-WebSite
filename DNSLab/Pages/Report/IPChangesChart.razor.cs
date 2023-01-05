using System.Drawing;

namespace DNSLab.Pages.Report;
partial class IPChangesChart
{
    private BitChartBarConfig _stackedBarChartConfigExample;
    protected override async Task OnInitializedAsync()
    {
        await InitlineChartExample();
    }

    private async Task InitlineChartExample()
    {
        var last30DayIPChangesCount = await _DNSRepository.GetLast30DayIPChangesCount();
        _stackedBarChartConfigExample = new BitChartBarConfig { Options = new BitChartBarOptions { Responsive = true, MaintainAspectRatio = false, AspectRatio = 0.8, Title = new BitChartOptionsTitle { Display = true, Text = "نمودار تعداد دفعات تغییرات آی پی به تفکیک روز", FontFamily = "vazir-regular", }, Tooltips = new BitChartTooltips { Mode = BitChartInteractionMode.Nearest, Intersect = true, TitleFontFamily = "vazir-regular", }, Hover = new BitChartHover { Mode = BitChartInteractionMode.Nearest, Intersect = true }, Scales = new BitChartBarScales { XAxes = new List<BitChartCartesianAxis> { new BitChartBarCategoryAxis { Stacked = true, } }, YAxes = new List<BitChartCartesianAxis> { new BitChartBarLinearCartesianAxis { Stacked = true } } } } };
        for (int i = -30; i <= 0; i++)
        {
            var date = DateTime.UtcNow.AddDays(i).Date;
            foreach (var item in last30DayIPChangesCount)
            {
                if (!item.DateAndCount.Any(x => x.Key == date))
                    item.DateAndCount.Add(date, 0);
            }

            _stackedBarChartConfigExample.Data.Labels.Add(date.ToLocalizerString().Substring(0, 10));
        }

        var haveChangeRecords = last30DayIPChangesCount.Where(x => x.DateAndCount.Any(x => x.Value > 0));
        Color[] colors = new Color[] { Color.MediumVioletRed, Color.MediumSpringGreen, Color.DeepSkyBlue, Color.Coral, Color.OliveDrab, Color.DimGray, Color.Indigo };
        for (int i = 0; i < haveChangeRecords.Count(); i++)
        {
            _stackedBarChartConfigExample.Data.Datasets.Add(new BitChartBarDataset<int>(haveChangeRecords.ElementAt(i).DateAndCount.OrderBy(x => x.Key).Select(x => x.Value).AsEnumerable())
            { Label = haveChangeRecords.ElementAt(i).HostName, BackgroundColor = BitChartColorUtil.FromDrawingColor(colors[i]) });
        }
    }
}