using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class ReplyComment
{
    [Parameter] public Guid ReplyTo { get; set; }

    private CreateCommentDTO comment = new CreateCommentDTO { Text = String.Empty };

    public async Task Reply()
    {
        comment.ReplyTo = ReplyTo;
        if (await commentRepository.AddNewComment(comment))
        {
            _navManager.NavigateTo("Comment/ReviewComments");
            toastService.ShowToast(localizer["ReplySent"], Enums.ToastLevel.Success);
        }
    }
}
