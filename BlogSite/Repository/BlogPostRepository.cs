using BlogSite.Data;
using BlogSite.Models;
using BlogSite.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(BlogPost post)
        {
            _context.Add(post);
            return Save();
        }

        public bool Delete(BlogPost post)
        {
            _context.Remove(post);
            return Save();
        }

        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            return await _context.BlogPosts
                 .Include(p => p.Comments).ToListAsync();
        }

        public async Task<BlogPost> GetById(int id)
        {
            return await _context.BlogPosts.Where(b => b.Id == id)
                .Include(p => p.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogPost> GetByIdTracking(int id)
        {
            return await _context.BlogPosts
                .Where(b => b.Id == id)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(BlogPost post)
        {
            _context.Update(post);
            return Save();
        }
    }
}
