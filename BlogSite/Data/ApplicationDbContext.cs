using BlogSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comments> PostComments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
