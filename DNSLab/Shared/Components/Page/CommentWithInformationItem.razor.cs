using DNSLab.DTOs.Comments;

namespace DNSLab.Shared.Components.Page;
partial class CommentWithInformationItem
{
    [Parameter] public PageCommentWithInformationDTO PageCommentWithInformation { get; set; }
    [Parameter] public EventCallback AcceptButtonClick { get; set; }
    [Parameter] public EventCallback RejectButtonClick { get; set; }
    [Parameter] public bool? IsApproved { get; set; }
}
