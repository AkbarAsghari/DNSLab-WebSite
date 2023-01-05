using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class CreateComment
{
    private CommentDTO createComment = new CommentDTO { Text = String.Empty };

    public async Task Create()
    {
        if (await commentRepository.AddNewComment(createComment))
        {
            _navManager.NavigateTo("Comment/MyComments");
            toastService.ShowToast(localizer["CommentSubmited"], Enums.ToastLevel.Success);
        }
    }
}
