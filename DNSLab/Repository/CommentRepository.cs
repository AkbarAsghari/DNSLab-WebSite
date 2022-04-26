using DNSLab.DTOs.Comments;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IHttpService _httpService;
        public CommentRepository(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<bool> AddNewComment(CreateCommentDTO comment)
        {
            var result = await _httpService.Post<CreateCommentDTO, bool>($"/Comment/Create", comment);
            return result.Response;
        }

        public async Task<IEnumerable<FullCommentDTO>> GetAllComments()
        {
            var result = await _httpService.Get<IEnumerable<FullCommentDTO>>($"/Comment/GetAllComments");
            return result.Response;
        }

        public async Task<CommentDTO> GetComment(Guid id)
        {
            var result = await _httpService.Get<CommentDTO>($"/Comment/get?Id={id}");
            return result.Response;
        }

        public async Task<IEnumerable<CommentDTO>> GetMyComments()
        {
            var result = await _httpService.Get<IEnumerable<FullCommentDTO>>($"/Comment/GetMyComments");
            return result.Response;
        }

        public async Task<bool> RemoveComment(Guid id)
        {
            var result = await _httpService.Delete<bool>($"/Comment/Delete?Id={id}");
            return result.Response;
        }

        public async Task<bool> UpdateComment(CommentDTO comment)
        {
            var result = await _httpService.Put<CommentDTO, bool>($"/Comment/Update", comment);
            return result.Response;
        }
    }
}
