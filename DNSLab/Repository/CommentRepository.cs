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

        public async Task<bool> AddNewComment(CreateCommentDTO comment)
        {
            var result = await _httpService.Post<CreateCommentDTO, bool>($"/Comment/Create", comment);
            return result.Response;
        }

        public async Task<IEnumerable<FullCommentDTO>> GetAllComments()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.GetAllComments, out IEnumerable<FullCommentDTO> cacheValue))
            {

                var result = await _httpService.Get<IEnumerable<FullCommentDTO>>($"/Comment/GetAllComments");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                _memoryCache.Set(CacheKeyEnum.GetAllComments, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

        public async Task<CommentDTO> GetComment(Guid id)
        {
            var result = await _httpService.Get<CommentDTO>($"/Comment/get?Id={id}");
            return result.Response;
        }

        public async Task<IEnumerable<FullCommentDTO>> GetMyComments()
        {
            var result = await _httpService.Get<IEnumerable<FullCommentDTO>>($"/Comment/GetMyComments");
            return result.Response;
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            var result = await _httpService.Delete<bool>($"/Comment/Delete?Id={id}");
            return result.Response;
        }

        public async Task<bool> UpdateComment(CommentDTO comment)
        {
            var result = await _httpService.Put<CommentDTO, bool>($"/Comment/Update", comment);
            return result.Response;
        }

        public async Task<IEnumerable<CommentDTO>> GetNewComments()
        {
            var result = await _httpService.Get<IEnumerable<FullCommentDTO>>($"/Comment/GetNewComments");
            return result.Response;
        }

        public async Task<bool> ReviewComment(Guid id, bool isApproved)
        {
            var result = await _httpService.Post<bool>($"/Comment/ReviewComment?Id={id}&isApprove={isApproved}");
            return result.Response;
        }

        public async Task<IEnumerable<FullCommentDTO>> GetCommentReplies(Guid id)
        {
            var result = await _httpService.Get<IEnumerable<FullCommentDTO>>($"/Comment/GetCommentReplies?commentId={id}");
            return result.Response;
        }

        public async Task<int> GetCommentsNotSeenRepliesCount()
        {
            var result = await _httpService.Get<int>($"/Comment/GetCommentsNotSeenRepliesCount");
            return result.Response;
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
    }
}
