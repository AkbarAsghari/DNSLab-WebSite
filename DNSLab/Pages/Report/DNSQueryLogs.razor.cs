using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;
using DNSLab.Enums.DNSQueryLogs;
using MudBlazor;
using System.Globalization;
using System.Reflection;

namespace DNSLab.Pages.Report;

partial class DNSQueryLogs
{
    Guid SelectedHostName { get; set; }
    bool OrderByDESC { get; set; } = true;
    int PageNumber { get; set; } = 1;
    int EntriesPerPage { get; set; } = 10;
    private DateRange SelectedDateRange = new DateRange(DateTime.Now.Date, DateTime.Now.Date);
    string? ClientIpAddress { get; set; }
    ProtocolEnum? SelectedProtocol { get; set; }
    ResponseTypeEnum? SelectedResponseType { get; set; }
    RCodeEnum? SelectedRCode { get; set; }
    RecordTypeEnum? SelectedRecordType { get; set; }
    ClassEnum? SelectedClass { get; set; }

    IEnumerable<HostSummaryDTO> HostNames;
    QueryLogsResponseDTO Logs;


    protected override async Task OnInitializedAsync()
    {
        HostNames = await dnsRepository.GetOwnHostNamesDomain();
        if (HostNames.Count() > 0)
        {
            SelectedHostName = HostNames.First().Id;
        }
    }


    public async Task Search()
    {
        DateTime? fromDate = null, toDate = null;

        if (SelectedDateRange.Start.HasValue)
        {
            fromDate = new DateTime(SelectedDateRange.Start.Value.Year,
                SelectedDateRange.Start.Value.Month,
                SelectedDateRange.Start.Value.Day,
                00, 00, 00);
        }

        if (SelectedDateRange.End.HasValue)
        {
            toDate = new DateTime(SelectedDateRange.End.Value.Year,
               SelectedDateRange.End.Value.Month,
               SelectedDateRange.End.Value.Day,
               23, 59, 59);
        }

        Logs = await dnsRepository.QueryLogs(SelectedHostName,
            PageNumber,
            EntriesPerPage,
            OrderByDESC,
            fromDate,
            toDate,
            ClientIpAddress,
            SelectedProtocol,
            SelectedResponseType,
            SelectedRCode,
            SelectedRecordType,
            SelectedClass);
    }

    private async Task SelectedPageChange(int page)
    {
        PageNumber = page;
        await Search();
    }

    public IMask ipv4Mask = RegexMask.IPv4();

    public CultureInfo GetPersianCulture()
    {
        var culture = new CultureInfo("fa-IR");
        DateTimeFormatInfo formatInfo = culture.DateTimeFormat;
        formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
        formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
        var monthNames = new[]
        {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
            "اسفند",
            "",
        };
        formatInfo.AbbreviatedMonthNames =
            formatInfo.MonthNames =
                formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
        formatInfo.AMDesignator = "ق.ظ";
        formatInfo.PMDesignator = "ب.ظ";
        formatInfo.ShortDatePattern = "yyyy/MM/dd";
        formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
        formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
        System.Globalization.Calendar cal = new PersianCalendar();
        FieldInfo fieldInfo = culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (fieldInfo != null)
            fieldInfo.SetValue(culture, cal);
        FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (info != null)
            info.SetValue(formatInfo, cal);
        culture.NumberFormat.NumberDecimalSeparator = "/";
        culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
        culture.NumberFormat.NumberNegativePattern = 0;
        return culture;
    }
}
