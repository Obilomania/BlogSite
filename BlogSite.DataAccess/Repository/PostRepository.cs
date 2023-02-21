using BlogSite.DataAccess;
using BlogSite.DataAccess.Repository.IRepository;
using BlogSite.Models;

namespace BlogSite.DataAccess.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Comments postComment)
        {
            _context.Add(postComment);
            return Save();
        }

        public bool Delete(Comments postComment)
        {
            _context.Remove(postComment);
            return Save();
        }

        public Task<IEnumerable<Comments>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Comments> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comments>> GetCommentByPost(int id)
        {
            return _context.BlogPosts
                .Where(b => b.Id == id)
                .FirstOrDefault().Comments;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Comments postComment)
        {
            throw new NotImplementedException();
        }
    }
}
