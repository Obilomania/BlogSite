using BlogSite.Models;

namespace BlogSite.Repository.IRepository
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAll();
        Task<BlogPost> GetById(int id);
        Task<BlogPost> GetByIdTracking(int id);
        bool Add(BlogPost post);
        bool Update(BlogPost post); 
        bool Delete(BlogPost post);
        bool Save();
    }
}
