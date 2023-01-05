namespace DNSLab.Shared;
partial class TimeLine<TItem>
{
    [Parameter] public string TimeLineHeader { get; set; }
    [Parameter] public IEnumerable<TItem> Data { get; set; }
    [Parameter] public RenderFragment<TItem> TimeLineItemRenderFragment { get; set; }
}
