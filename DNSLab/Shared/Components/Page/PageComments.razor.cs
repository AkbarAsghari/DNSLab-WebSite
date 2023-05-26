using DNSLab.DTOs.Comments;

namespace DNSLab.Shared.Components.Page;
partial class PageComments
{
    [Parameter] public Guid PageId { get; set; }
    [Parameter] public bool ShowAll { get; set; } = false;

    Guid CurrentUserId = Guid.Empty;
    Guid ReplyToId = Guid.Empty;
    Guid BaseCommentId = Guid.Empty;
    
    int totalCommentsCount = 0;

    Modal AddNewPageCommentModal;
    CreatePageCommentDTO NewPageComment = new CreatePageCommentDTO { Text = String.Empty };

    IEnumerable<PageCommentAndRepliesDTO> PageCommentAndReplies;

    protected override async Task OnInitializedAsync()
    {
        PageCommentAndReplies = await _CommentRepository.GetPageComments(PageId);

        totalCommentsCount = PageCommentAndReplies.Count();

        if (!ShowAll)
            PageCommentAndReplies = PageCommentAndReplies.OrderByDescending(x => x.Comment.CreateDate).Take(3);

        var user = (await auth.GetAuthenticationStateAsync()).User;

        if (user.Identity != null)
            CurrentUserId = Guid.Parse(user.Identities.FirstOrDefault()!.Claims.FirstOrDefault(x => x.Type.ToLower() == "nameid")!.Value);
    }

    async Task AddNewPageComment(PageCommentDTO replyTo = null)
    {
        if (replyTo == null)
            ReplyToId = Guid.Empty;
        else
        {
            ReplyToId = replyTo.Id;
            BaseCommentId = replyTo.ReplyTo == null ? replyTo.Id : replyTo.BaseCommentId!.Value;
        }


        if (CurrentUserId == Guid.Empty)
            _NavigationManager.NavigateTo($"/user/auth?RedirectTo={new Uri(_NavigationManager.Uri).AbsolutePath}");
        else
            await AddNewPageCommentModal.Open();
    }

    async Task SubmitComment()
    {
        NewPageComment.PageId = PageId;

        if (ReplyToId != Guid.Empty)
        {
            NewPageComment.ReplyTo = ReplyToId;
            NewPageComment.BaseCommentId = BaseCommentId;
        }

        if (await _CommentRepository.AddNewPageComment(NewPageComment))
        {
            NewPageComment.Text = String.Empty;
            NewPageComment.ReplyTo = Guid.Empty;
            NewPageComment.BaseCommentId = Guid.Empty;

            await AddNewPageCommentModal.Close();
            await OnInitializedAsync();
        }
    }
}
