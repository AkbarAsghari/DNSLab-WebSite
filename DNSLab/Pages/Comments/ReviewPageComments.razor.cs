using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class ReviewPageComments
{
    List<PageCommentWithInformationDTO> comments = new();

    private bool? IsApprovedKey = null;

    protected override async Task OnInitializedAsync()
    {
        await FillCommentsList();
    }

    async Task FillCommentsList()
    {
        comments = (await _CommentRepository.GetAllPageCommentsWithInformation(IsApprovedKey)).ToList();
    }

    async Task PivotOnLinkClick(BitPivotItem item)
    {
        switch (item.Key)
        {
            case "NotReviewComments":
                IsApprovedKey = null;
                break;
            case "AllApprovedComments":
                IsApprovedKey = true;
                break;
            case "AllNotApprovedComments":
                IsApprovedKey = false;
                break;
        }
        await FillCommentsList();
    }

    async Task ReviewComment(Guid commentId, bool isApproved)
    {
        if (await _CommentRepository.ReviewPageComment(commentId, isApproved))
            comments.Remove(comments.First(x => x.Comment.Id == commentId));
    }
}
