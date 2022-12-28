using DNSLab.DTOs.Comments;

namespace DNSLab.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task<bool> AddNewComment(CreateCommentDTO comment);
        Task<CommentDTO> GetComment(Guid id);
        Task<IEnumerable<FullCommentDTO>> GetMyComments();
        Task<IEnumerable<CommentDTO>> GetNewComments();
        Task<IEnumerable<FullCommentDTO>> GetAllComments();
        Task<IEnumerable<FullCommentDTO>> GetCommentReplies(Guid id);
        Task<int> GetCommentsNotSeenRepliesCount();
        Task<bool> DeleteComment(Guid id);
        Task<bool> UpdateComment(CommentDTO comment);
        Task<bool> ReviewComment(Guid id, bool isApproved);
        //Page
        Task<bool> AddNewPageComment(CreatePageCommentDTO pageComment);
        Task<bool> RemovePageComment(Guid commentId);
        Task<IEnumerable<PageCommentAndRepliesDTO>> GetPageComments(Guid pageId);
        Task<bool> UpdatePageComment(PageCommentDTO comment);
        Task<PageCommentDTO> GetPageComment(Guid commentId);
        Task<bool> ReviewPageComment(Guid commentId, bool isApproved);
        Task<IEnumerable<PageCommentWithInformationDTO>> GetAllPageCommentsWithInformation(bool? isApproved = null);
    }
}
