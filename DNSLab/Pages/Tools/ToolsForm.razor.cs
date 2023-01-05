namespace DNSLab.Pages.Tools;

partial class ToolsForm<TItem>
{
    [Parameter] public TItem Model { get; set; }
    [Parameter] public string Header { get; set; }
    [Parameter] public RenderFragment Body { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public string Result { get; set; }
    [Parameter] public bool IsProgressing { get; set; }
}
