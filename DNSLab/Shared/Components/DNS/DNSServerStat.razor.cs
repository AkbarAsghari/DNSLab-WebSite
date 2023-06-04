using DNSLab.DTOs.Statics;
using DNSLab.Helper.Utilities;

namespace DNSLab.Shared.Components.DNS;
partial class DNSServerStat
{
    private StatResponse _StatResponse = null;
    private StatTypeEnum StatType = StatTypeEnum.LastHour;
    private BitDateRangePicker rangeDatePicker;

    protected override async Task OnInitializedAsync()
    {
        InitlineChartExample();
        InitPieChartQueryResponse();
        InitPieChartQueryType();

        new Timer(new TimerCallback(async _ =>
        {
            await GenerateReport();
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }), null, 0, 60 * 1000);
    }

    async Task StatTypeOnChange(string value)
    {
        if (String.IsNullOrEmpty(value))
            value = StatTypeEnum.LastHour.ToString();
        StatType = (StatTypeEnum)System.Enum.Parse(typeof(StatTypeEnum), value);

        await GenerateReport();
    }

    async Task GenerateReport()
    {
        if (StatType == StatTypeEnum.Custom && (rangeDatePicker == null || rangeDatePicker.Value!.StartDate == null || rangeDatePicker.Value.EndDate == null))
            return;
        else if (StatType == StatTypeEnum.Custom && rangeDatePicker != null)
            _StatResponse = await _StaticRepository.GetStat(StatType, rangeDatePicker.Value!.StartDate!.Value.DateTime, rangeDatePicker.Value.EndDate!.Value.DateTime);
        else
            _StatResponse = await _StaticRepository.GetStat(StatType);


        BindLineChartData();
        BindPieChartDataQueryResponse();
        BindPieChartDataQueryType();

        if (_lineChart != null)
            await _lineChart.Update();
        if (_pieChartQueryResponse != null)
            await _pieChartQueryResponse.Update();
        if (_pieChartQueryType != null)
            await _pieChartQueryType.Update();
    }

    private List<BitDropDownItem> GetStatTypeItems()
    {
        List<BitDropDownItem> result = new List<BitDropDownItem>();
        foreach (var item in Enum.GetValues(typeof(StatTypeEnum)))
            result.Add(new BitDropDownItem { Text = item.ToString()!, Value = item.ToString()! });
        return result;
    }

    string GetPrecentage(int total, int current) => (total == 0 ? 0 : ((current * 100 / total))).ToString().EnglishToPersianNumbers() + "%";

    #region LineChart
    private BitChart _lineChart;
    private BitChartLineConfig _lineChartConfig;

    void InitlineChartExample()
    {
        _lineChartConfig = new BitChartLineConfig
        {
            Options = new BitChartLineOptions
            {
                Responsive = true,
                Animation = new BitChartAnimation
                {
                    Duration = 1200,
                },
                Tooltips = new BitChartTooltips
                {
                    Mode = BitChartInteractionMode.Nearest,
                    Intersect = true
                },
                Hover = new BitChartHover
                {
                    Mode = BitChartInteractionMode.Nearest,
                    Intersect = true
                },
            }
        };
    }
    void BindLineChartData()
    {

        _lineChartConfig.Data.Labels.Clear();
        _lineChartConfig.Data.Datasets.Clear();

        foreach (var dataset in _StatResponse.Response.MainChartData.Datasets)
        {
            _lineChartConfig.Data.Datasets.Add(new BitChartLineDataset<int>(dataset.Data)
            {
                Label = dataset.Label,
                BackgroundColor = BitChartColorUtil.FromDrawingColor(ColorHelper.ParseColor(dataset.BackgroundColor)),
                BorderColor = BitChartColorUtil.FromDrawingColor(ColorHelper.ParseColor(dataset.BorderColor)),
                Fill = dataset.Fill,
                BorderWidth = 1
            });
        }

        foreach (var label in _StatResponse.Response.MainChartData.Labels)
            _lineChartConfig.Data.Labels.Add(label.ToLocalTime().ToString(_StatResponse.Response.MainChartData.LabelFormat.Replace("DD", "dd")));

    }
    #endregion

    #region PieCharQueryResponse
    private BitChartPieConfig _pieChartConfigQueryResponse;
    private BitChart _pieChartQueryResponse;

    private void InitPieChartQueryResponse()
    {
        _pieChartConfigQueryResponse = new BitChartPieConfig
        {
            Options = new BitChartPieOptions
            {
                Responsive = true,
            }
        };
    }

    void BindPieChartDataQueryResponse()
    {

        _pieChartConfigQueryResponse.Data.Labels.Clear();
        _pieChartConfigQueryResponse.Data.Datasets.Clear();

        BitChartPieDataset<int>
    dataset = new BitChartPieDataset<int>
        (_StatResponse.Response.QueryResponseChartData.Datasets[0].Data)
    {
        BackgroundColor = _StatResponse.Response.QueryResponseChartData.Datasets[0].BackgroundColor.Select(x => BitChartColorUtil.FromDrawingColor(ColorHelper.ParseColor(x))).ToArray()
    };

        foreach (var label in _StatResponse.Response.QueryResponseChartData.Labels)
        {
            _pieChartConfigQueryResponse.Data.Labels.Add(label);
        }
        _pieChartConfigQueryResponse.Data.Datasets.Add(dataset);
    }

    #endregion

    #region PieCharQueryType

    private BitChartPieConfig _pieChartConfigQueryType;
    private BitChart _pieChartQueryType;

    private void InitPieChartQueryType()
    {
        _pieChartConfigQueryType = new BitChartPieConfig
        {
            Options = new BitChartPieOptions
            {
                Responsive = true,
            }
        };
    }

    void BindPieChartDataQueryType()
    {

        _pieChartConfigQueryType.Data.Labels.Clear();
        _pieChartConfigQueryType.Data.Datasets.Clear();

        BitChartPieDataset<int>
            dataset = new BitChartPieDataset<int>
                (_StatResponse.Response.Querytypechartdata.Datasets[0].Data)
            {
                BackgroundColor = _StatResponse.Response.Querytypechartdata.Datasets[0].BackgroundColor.Select(x => BitChartColorUtil.FromDrawingColor(ColorHelper.ParseColor(x))).ToArray()
            };

        foreach (var label in _StatResponse.Response.Querytypechartdata.Labels)
        {
            _pieChartConfigQueryType.Data.Labels.Add(label);
        }
        _pieChartConfigQueryType.Data.Datasets.Add(dataset);
    }

    #endregion
}
