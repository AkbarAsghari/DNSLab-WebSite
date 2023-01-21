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
        Replies.ToList().ForEach(x => x.Text = x.Text.ChangeUrlsToLink());
        isCollapsed = !isCollapsed;
    }
}
