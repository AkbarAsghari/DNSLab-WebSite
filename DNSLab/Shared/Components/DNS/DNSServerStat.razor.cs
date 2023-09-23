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
    int QuerytypeSelectedIndex = 0;
    double[]? QuerytypechartData;
    string[]? QuerytypechartLabels;

    //PieChart
    double[]? QueryResponsechartData;
    string[]? QueryResponsechartLabels;

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
        options.LineStrokeWidth = 0.2D;
        XAxisLabels = null;
        Series = new List<ChartSeries>();
    }

    void InitPieChart()
    {
        QuerytypechartData = null;
        QuerytypechartLabels = null;
        QueryResponsechartData = null;
        QueryResponsechartLabels = null;
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
        BindQueryTypePieChartData();
        BindQueryResponsePieChartData();

        if (StatType != StatTypeEnum.LastHour)
            AutoRefresher(false);
    }

    string GetPrecentage(int total, int current) => (total == 0 ? 0 : ((current * 100 / total))).ToString().EnglishToPersianNumbers() + "%";


    void BindLineChartData()
    {
        XAxisLabels = new string[_StatResponse.Response.MainChartData.Labels.Length];

        foreach (var dataset in _StatResponse.Response.MainChartData.Datasets)
        {
            Series.Add(new ChartSeries
            {
                Name = dataset.Label,
                Data = dataset.Data.Select(x => Convert.ToDouble(x)).ToArray()
            });
        }

        for (int i = 0; i < XAxisLabels.Length; i++)
            XAxisLabels[i] = _StatResponse.Response.MainChartData.Labels[i].ToLocalTime().ToString(_StatResponse.Response.MainChartData.LabelFormat.Replace("DD", "dd"));
    }

    void BindQueryTypePieChartData()
    {
        QuerytypechartLabels = new string[_StatResponse.Response.Querytypechartdata.Labels.Length];
        QuerytypechartData = new double[_StatResponse.Response.Querytypechartdata.Datasets[0].Data.Length];

        for (int i = 0; i < QuerytypechartData.Length; i++)
        {
            QuerytypechartData[i] = (_StatResponse.Response.Querytypechartdata.Datasets[0].Data[i]);
        }
        for (int i = 0; i < QuerytypechartLabels.Count(); i++)
            QuerytypechartLabels[i] = _StatResponse.Response.Querytypechartdata.Labels[i];
    }

    void BindQueryResponsePieChartData()
    {
        QueryResponsechartLabels = new string[_StatResponse.Response.QueryResponseChartData.Labels.Length];
        QueryResponsechartData = new double[_StatResponse.Response.QueryResponseChartData.Datasets[0].Data.Length];

        for (int i = 0; i < QueryResponsechartData.Length; i++)
        {
            QueryResponsechartData[i] = (_StatResponse.Response.QueryResponseChartData.Datasets[0].Data[i]);
        }
        for (int i = 0; i < QueryResponsechartLabels.Count(); i++)
            QueryResponsechartLabels[i] = _StatResponse.Response.QueryResponseChartData.Labels[i];
    }

    public void Dispose()
    {
        if (_timer != null)
            _timer.Dispose();
    }
}
