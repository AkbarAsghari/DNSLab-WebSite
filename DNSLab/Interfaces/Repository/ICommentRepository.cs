using DNSLab.DTOs.Comments;

namespace DNSLab.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task<bool> AddNewComment(CreateCommentDTO comment);
        Task<CommentDTO> GetComment(Guid id);
        Task<IEnumerable<CommentDTO>> GetMyComments();
        Task<IEnumerable<CommentDTO>> GetNewComments();
        Task<IEnumerable<FullCommentDTO>> GetAllComments();
        Task<IEnumerable<FullCommentDTO>> GetCommentReplies(Guid id);
        Task<int> GetCommentsNotSeenRepliesCount();
        Task<bool> DeleteComment(Guid id);
        Task<bool> UpdateComment(CommentDTO comment);
        Task<bool> ReviewComment(Guid id, bool isApproved);
    }
}
