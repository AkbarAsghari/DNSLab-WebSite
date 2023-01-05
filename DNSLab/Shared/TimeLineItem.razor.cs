namespace DNSLab.Shared;
partial class TimeLineItem
{
    [Parameter] public string? Header { get; set; }
    [Parameter] public RenderFragment Body { get; set; }
    [Parameter] public bool ShowBodyWithAccordion { get; set; } = false;
    [Parameter] public DateTime? DateTime { get; set; }
}
