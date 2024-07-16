using ApexCharts;
using DNSLab.DTOs.Statics;
using DNSLab.Helper.Utilities;
using MudBlazor;
using MudBlazor.Utilities;

namespace DNSLab.Components.Shared.Components.DNS;
partial class DNSServerStat : IDisposable
{
    [CascadingParameter] public bool _isDarkMode { get; set; } = false;
    class ChartData
    {
        public string Lable { get; set; }
        public int Value { get; set; }
    }

    class QueryChartData
    {
        public string QuertType { get; set; }
        public List<ChartData> ChartDatas { get; set; }
    }

    List<QueryChartData> QueryChartDatas = new List<QueryChartData>();
    ApexChart<ChartData> LineChart;

    //PieChart

    class QueryTypeData
    {
        public string QueryType { get; set; }
        public int Count { get; set; }
    }

    List<QueryTypeData> QueryTypeChartData = new List<QueryTypeData>();
    ApexChart<QueryTypeData> QueryTypePieChart;

    private StatResponse _StatResponse = null;
    private StatTypeEnum StatType = StatTypeEnum.LastHour;
    private Timer _timer;
    private bool _autoRefresh = false;

    protected override async Task OnInitializedAsync()
    {
        await GenerateReport();
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

        await BindLineChartData();
        await BindQueryTypePieChartData();

        if (StatType != StatTypeEnum.LastHour)
            AutoRefresher(false);
    }

    async Task BindLineChartData()
    {
        QueryChartDatas.Clear();

        for (int i = 0; i < _StatResponse.Response.MainChartData.Datasets.Length; i++)
        {
            QueryChartData queryChartData = new QueryChartData()
            {
                QuertType = _StatResponse.Response.MainChartData.Datasets[i].Label,
                ChartDatas = new List<ChartData>()
            };

            for (int j = 0; j < _StatResponse.Response.MainChartData.Datasets[i].Data.Count(); j++)
            {
                queryChartData.ChartDatas.Add(
                       new ChartData
                       {
                           Lable = _StatResponse.Response.MainChartData.Labels[j].ToLocalTime().ToString(_StatResponse.Response.MainChartData.LabelFormat.Replace("DD", "dd").Replace("YYYY", "yyyy")),
                           Value = _StatResponse.Response.MainChartData.Datasets[i].Data[j]
                       });
            }
            QueryChartDatas.Add(queryChartData);
        }
        await InvokeAsync(() => StateHasChanged());
        if (LineChart != null)
        {
            await LineChart.UpdateSeriesAsync();
        }
    }

    async Task BindQueryTypePieChartData()
    {
        QueryTypeChartData.Clear();

        for (int i = 0; i < _StatResponse.Response.Querytypechartdata.Datasets[0].Data.Length; i++)
        {
            QueryTypeChartData.Add(new QueryTypeData
            {
                QueryType = _StatResponse.Response.Querytypechartdata.Labels[i],
                Count = _StatResponse.Response.Querytypechartdata.Datasets[0].Data[i]
            });
        }
        await InvokeAsync(() => StateHasChanged());
        if (QueryTypePieChart != null)
        {
            await QueryTypePieChart.UpdateSeriesAsync();
        }
    }
    public void Dispose()
    {
        if (_timer != null)
            _timer.Dispose();
    }
}
