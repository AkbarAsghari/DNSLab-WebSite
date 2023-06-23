using DNSLab.DTOs.Comments;
using static MudBlazor.CategoryTypes;

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

    async Task PanelChanged(int index)
    {
        {
            switch (index)
            {
                case 0:
                    IsApprovedKey = null;
                    break;
                case 1:
                    IsApprovedKey = true;
                    break;
                case 2:
                    IsApprovedKey = false;
                    break;
            }
            await FillCommentsList();
        }
    }

    async Task ReviewComment(Guid commentId, bool isApproved)
    {
        if (await _CommentRepository.ReviewPageComment(commentId, isApproved))
            comments.Remove(comments.First(x => x.Comment.Id == commentId));
    }
}
