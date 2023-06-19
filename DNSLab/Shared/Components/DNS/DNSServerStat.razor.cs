using DNSLab.DTOs.Statics;
using DNSLab.Helper.Utilities;
using MudBlazor;

namespace DNSLab.Shared.Components.DNS;
partial class DNSServerStat : IDisposable
{

    //LineChart
    private ChartOptions options = new ChartOptions();
    public List<ChartSeries> Series;
    public string[]? XAxisLabels;

    //PieChart
    int dataSize = 4;
    double[]? data;
    string[]? labels;

    Random random = new Random();

    private StatResponse _StatResponse = null;
    private StatTypeEnum StatType = StatTypeEnum.LastHour;
    private Timer _timer;
    private bool _autoRefresh = false;

    protected override async Task OnInitializedAsync()
    {
        await GenerateReport();
    }
    void InitLineChart()
    {
        options.InterpolationOption = InterpolationOption.NaturalSpline;
        options.LineStrokeWidth = 0.5D;
        XAxisLabels = null;
        Series = new List<ChartSeries>();
    }

    void InitPieChart()
    {
        data = null;
        labels = null;
    }

    void AutoRefresher(bool value)
    {
        _autoRefresh = value;
        if (value)
        {
            _timer = new Timer(new TimerCallback(async _ =>
            {
                await GenerateReport();
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }), null, 0, 60 * 1000);
        }
        else
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

    }

    async Task StatTypeOnChange(string value)
    {
        if (String.IsNullOrEmpty(value))
            value = StatTypeEnum.LastHour.ToString();
        StatType = (StatTypeEnum)System.Enum.Parse(typeof(StatTypeEnum), value);

        await GenerateReport();
    }
    DateRange dateRange;
    async Task DateRangeChanged(DateRange range)
    {
        dateRange = range;
        await GenerateReport();
    }

    async Task GenerateReport()
    {
        if (StatType == StatTypeEnum.Custom && (dateRange == null || dateRange.Start == null || dateRange.End == null))
            return;
        else if (StatType == StatTypeEnum.Custom && dateRange != null)
            _StatResponse = await _StaticRepository.GetStat(StatType, dateRange.Start, dateRange.End);
        else
            _StatResponse = await _StaticRepository.GetStat(StatType);

        InitLineChart();
        InitPieChart();

        BindLineChartData();
        BindPieChartData();

        if (StatType != StatTypeEnum.LastHour)
            AutoRefresher(false);
    }

    private List<BitDropDownItem> GetStatTypeItems()
    {
        List<BitDropDownItem> result = new List<BitDropDownItem>();
        foreach (var item in Enum.GetValues(typeof(StatTypeEnum)))
            result.Add(new BitDropDownItem { Text = item.ToString()!, Value = item.ToString()! });
        return result;
    }

    string GetPrecentage(int total, int current) => (total == 0 ? 0 : ((current * 100 / total))).ToString().EnglishToPersianNumbers() + "%";


    void BindLineChartData()
    {
        XAxisLabels = new string[_StatResponse.Response.MainChartData.Labels.Count()];

        foreach (var dataset in _StatResponse.Response.MainChartData.Datasets)
        {
            Series.Add(new ChartSeries
            {
                Name = dataset.Label,
                Data = dataset.Data.Select(x => Convert.ToDouble(x)).ToArray()
            });
        }

        for (int i = 0; i < XAxisLabels.Count(); i++)
            XAxisLabels[i] = _StatResponse.Response.MainChartData.Labels[i].ToLocalTime().ToString(_StatResponse.Response.MainChartData.LabelFormat.Replace("DD", "dd"));

    }

    void BindPieChartData()
    {
        XAxisLabels = new string[_StatResponse.Response.MainChartData.Labels.Count()];

        foreach (var dataset in _StatResponse.Response.MainChartData.Datasets)
        {
            Series.Add(new ChartSeries
            {
                Name = dataset.Label,
                Data = dataset.Data.Select(x => Convert.ToDouble(x)).ToArray()
            });
        }

        for (int i = 0; i < XAxisLabels.Count(); i++)
            XAxisLabels[i] = _StatResponse.Response.MainChartData.Labels[i].ToLocalTime().ToString(_StatResponse.Response.MainChartData.LabelFormat.Replace("DD", "dd"));

    }
    
    public void Dispose()
    {
        if (_timer != null)
            _timer.Dispose();
    }
}
