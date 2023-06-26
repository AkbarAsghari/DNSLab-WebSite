namespace DNSLab.Pages.Tools.MainTools;

partial class ToolsForm<TItem>
{
    [Parameter] public TItem Model { get; set; }
    [Parameter] public string Header { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public List<string> Results { get; set; }
    [Parameter] public bool IsProgressing { get; set; }
}
