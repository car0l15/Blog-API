using Microsoft.EntityFrameworkCore;
using projeto_final.Models;

namespace projeto_final.Repository

{
    public interface IBlogContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}