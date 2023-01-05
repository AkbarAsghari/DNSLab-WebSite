using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class CommentForm
{
    const int MAX_TEXT_COUNT = 500;
    [Parameter] public string Title { get; set; } = String.Empty;
    [Parameter] public CreateCommentDTO comment { get; set; } = new CommentDTO { Text = String.Empty };
    [Parameter] public EventCallback OnValidSubmit { get; set; }
}
