﻿using DNSLab.DTOs.Comments;

namespace DNSLab.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task<bool> AddNewPageComment(CreatePageCommentDTO pageComment);
        Task<bool> RemovePageComment(Guid commentId);
        Task<IEnumerable<PageCommentAndRepliesDTO>> GetPageComments(Guid pageId);
        Task<bool> UpdatePageComment(PageCommentDTO comment);
        Task<PageCommentDTO> GetPageComment(Guid commentId);
        Task<bool> ReviewPageComment(Guid commentId, bool isApproved);
        Task<IEnumerable<PageCommentWithInformationDTO>> GetAllPageCommentsWithInformation(bool? isApproved = null);
    }
}
