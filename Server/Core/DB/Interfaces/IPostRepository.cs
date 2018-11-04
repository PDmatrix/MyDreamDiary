using System.Threading.Tasks;
using DB.OutputDto;

namespace DB.Interfaces
{
    public interface IPostRepository
    {
        Task<GetPostDtoOut> GetPostAsync(int id, string userId);
        Task<AddPostDtoOut> AddPostAsync(string userId, int dreamId, string title);
	    Task<CommentDtoOut> AddCommentAsync(string userId, int postId, string content);
	    Task<CommentDtoOut> GetCommentAsync(int id);
	    Task LikeAsync(int id, string userId);
    }
}