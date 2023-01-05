using DNSLab.DTOs.Comments;

namespace DNSLab.Shared.Components.Page;
partial class PageCommentItem
{
    [Parameter] public Guid AuthUserId { get; set; } = Guid.Empty;
    [Parameter] public PageCommentDTO PageComment { get; set; }
    [Parameter] public EventCallback ReplyButtonClick { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }

    bool IsEditing = false;
    bool IsDeleted = false;

    Modal DeleteModal;

    CreatePageCommentDTO ReplyPageComment = new CreatePageCommentDTO { Text = String.Empty };

    async Task DeleteComment() => await DeleteModal.Open();

    async Task SubmitEditComment()
    {
        if (await _CommentRepository.UpdatePageComment(PageComment))
        {
            IsEditing = false;
        }
    }

    async Task SubmitDeleteComment()
    {
        if (await _CommentRepository.RemovePageComment(PageComment.Id))
        {
            await DeleteModal.Close();
            IsDeleted = true;
        }
    }
}
