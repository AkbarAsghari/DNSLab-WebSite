using DNSLab.DTOs.Comments;

namespace DNSLab.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task<bool> AddNewComment(CreateCommentDTO comment);
        Task<CommentDTO> GetComment(Guid id);
        Task<IEnumerable<CommentDTO>> GetMyComments();
        Task<IEnumerable<FullCommentDTO>> GetAllComments();
        Task<bool> DeleteComment(Guid id);
        Task<bool> UpdateComment(CommentDTO comment);
    }
}
