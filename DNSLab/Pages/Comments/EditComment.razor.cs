using DNSLab.DTOs.Comments;

namespace DNSLab.Pages.Comments;
partial class EditComment
{
    [Parameter] public Guid CommentId { get; set; }

    private CommentDTO existComment;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            existComment = await commentRepository.GetComment(CommentId);
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    public async Task Update()
    {
        if (await commentRepository.UpdateComment(existComment))
        {
            _navManager.NavigateTo("Comment/MyComments");
            toastService.ShowToast(localizer["CommentUpdated"], Enums.ToastLevel.Success);
        }
    }
}
