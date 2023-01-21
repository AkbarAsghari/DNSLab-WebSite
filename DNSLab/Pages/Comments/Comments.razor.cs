using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class Comments
{
    IEnumerable<FullCommentDTO> allComments;
    protected override async Task OnInitializedAsync()
    {
        allComments = await commentRepository.GetAllComments();
    }
}
