using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class ReviewComments
{
    IEnumerable<CommentDTO> newComments;

    Modal DeclineModal { get; set; }
    CommentDTO DeclineRcord { get; set; } = new();

    Modal DetailsModal { get; set; }
    CommentDTO DetailsRcord { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadComments();
        }
    }

    private async Task LoadComments()
    {
        newComments = await commentRepository.GetNewComments();
        await this.InvokeAsync(() => StateHasChanged());
        await DeclineModal.Close();
        await DetailsModal.Close();
    }

    private async Task OpenDeclineModal(CommentDTO record)
    {
        DeclineRcord = record;
        await DeclineModal.Open();
    }

    private async Task ShowCommentDetails(CommentDTO record)
    {
        DetailsRcord = record;
        await DetailsModal.Open();
    }

    private async Task ReplyComment(CommentDTO record)
    {
        NavigationManager.NavigateTo("Comment/Reply/" + record.Id);
    }
    private async Task AcceptDecline()
    {
        await ReviewComment(DeclineRcord, false);
    }
    private async Task ReviewComment(CommentDTO record, bool isApproved)
    {
        if (await commentRepository.ReviewComment(record.Id, isApproved))
            await LoadComments();
    }
}
