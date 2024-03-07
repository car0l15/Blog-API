using Microsoft.EntityFrameworkCore;
using projeto_final.Models;

namespace projeto_final.Repository

{
    public interface ITryBlogContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}