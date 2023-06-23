using DNSLab.DTOs.Comments;
using DNSLab.Repository;
using MudBlazor;

namespace DNSLab.Shared.Components.Page;
partial class PageCommentItem
{
    [Inject] private IDialogService DialogService { get; set; }

    [Parameter] public Guid AuthUserId { get; set; } = Guid.Empty;
    [Parameter] public PageCommentDTO PageComment { get; set; }
    [Parameter] public EventCallback ReplyButtonClick { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }

    bool IsEditing = false;
    bool IsDeleted = false;

    async Task DeleteComment()
    {
        bool? result = await DialogService.ShowMessageBox(
            "هشدار",
            $"آیا از حذف نظر ({PageComment.Text}) مطمئن هستید؟",
            yesText: "حذف", cancelText: "انصراف");

        if (result == true)
        {
            if (await _CommentRepository.RemovePageComment(PageComment.Id))
            {
                IsDeleted = true;
            }
        }
    }

    async Task SubmitEditComment()
    {
        if (await _CommentRepository.UpdatePageComment(PageComment))
        {
            IsEditing = false;
        }
    }
}
