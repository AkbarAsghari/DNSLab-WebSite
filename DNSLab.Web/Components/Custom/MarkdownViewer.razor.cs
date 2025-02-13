using Markdig;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Custom;

partial class MarkdownViewer
{
    [Parameter] public string MarkdownText { get; set; }

    string HTML
    {
        get
        {
            if (String.IsNullOrEmpty(MarkdownText))
            {
                return String.Empty;
            }

            return Markdown.ToHtml(MarkdownText, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
        }
    }
}
