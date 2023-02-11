using DNSLab.DTOs.Comments;
using DNSLab.Enums;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public CommentRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<bool> RemovePageComment(Guid commentId)
        {
            var result = await _httpService.Delete<bool>($"/Comment/RemovePageComment?Id={commentId}");
            return result.Response;
        }

        public async Task<bool> AddNewPageComment(CreatePageCommentDTO pageComment)
        {
            var result = await _httpService.Post<CreatePageCommentDTO, bool>($"/Comment/AddNewPageComment", pageComment);
            return result.Response;
        }

        public async Task<IEnumerable<PageCommentAndRepliesDTO>> GetPageComments(Guid pageId)
        {
            var result = await _httpService.Get<IEnumerable<PageCommentAndRepliesDTO>>($"/Comment/GetPageComments?pageId={pageId}");
            return result.Response;
        }

        public async Task<bool> UpdatePageComment(PageCommentDTO comment)
        {
            var result = await _httpService.Put<PageCommentDTO, bool>($"/Comment/UpdatePageComment", comment);
            return result.Response;
        }

        public async Task<PageCommentDTO> GetPageComment(Guid commentId)
        {
            var result = await _httpService.Get<PageCommentDTO>($"/Comment/GetPageComment?commentId={commentId}");
            return result.Response;
        }

        public async Task<IEnumerable<PageCommentWithInformationDTO>> GetAllPageCommentsWithInformation(bool? isApproved = null)
        {
            var result = await _httpService.Get<IEnumerable<PageCommentWithInformationDTO>>($"/Comment/GetAllPageCommentsWithInformation{(isApproved == null ? String.Empty : "?isApproved=" + isApproved)}");
            return result.Response;
        }

        public async Task<bool> ReviewPageComment(Guid commentId, bool isApproved)
        {
            var result = await _httpService.Put<bool>($"/Comment/ReviewPageComment?commentId={commentId}&isApprove={isApproved}");
            return result.Response;
        }
    }
}
