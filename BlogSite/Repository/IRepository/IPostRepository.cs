using BlogSite.Models;

namespace BlogSite.Repository.IRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Comments>> GetCommentByPost(int id);
        Task<IEnumerable<Comments>> GetAll();
        Task<Comments> GetById(int id);
        bool Add(Comments postComment);
        bool Update(Comments postComment);
        bool Delete(Comments postComment);
        bool Save();
    }
}
