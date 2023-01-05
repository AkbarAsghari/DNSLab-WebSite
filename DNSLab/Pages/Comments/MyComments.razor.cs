using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;

partial class MyComments
{
    IEnumerable<FullCommentDTO> myComments;
    IEnumerable<FullCommentDTO> commentReplies;

    Modal DeleteModal { get; set; }
    Modal RepliesModal { get; set; }

    CommentDTO deleteRcord { get; set; } = new();
    private bool isLoading = false;
    private bool isInProgress = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadComments();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task LoadComments()
    {
        isLoading = true;
        myComments = await commentRepository.GetMyComments();
        isLoading = false;
    }

    private async Task AcceptDelete()
    {
        isInProgress = true;
        if (deleteRcord != null)
        {
            if (await commentRepository.DeleteComment(deleteRcord.Id))
            {
                await DeleteModal.Close();
                await LoadComments();
            }
        }
        deleteRcord = new CommentDTO();

        isInProgress = false;
    }

    private async Task GetRepliesModal(CommentDTO record)
    {
        commentReplies = await commentRepository.GetCommentReplies(record.Id);
        await RepliesModal.Open();
    }

    private async Task DeleteComment(CommentDTO record)
    {
        deleteRcord = record;
        await DeleteModal.Open();
    }

    private async Task EditComment(CommentDTO record)
    {
        NavigationManager.NavigateTo("Comment/edit/" + record.Id);
    }
}
