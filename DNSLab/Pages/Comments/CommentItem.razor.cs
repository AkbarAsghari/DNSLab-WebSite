using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class CommentItem
{
    [Parameter] public FullCommentDTO FullComment { get; set; }
    private IEnumerable<FullCommentDTO> Replies { get; set; }
    private bool isCollapsed = false;

    private async Task ShowReplies()
    {
        if (Replies == null)
            Replies = await commentRepository.GetCommentReplies(FullComment.Id);

        isCollapsed = !isCollapsed;
    }
}
