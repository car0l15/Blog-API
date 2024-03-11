using Microsoft.EntityFrameworkCore;
using projeto_final.Models;
using projeto_final.Repository;

namespace Blog.Test;

public class BlogTestContext : DbContext, IBlogContext
{
    public BlogTestContext(DbContextOptions<BlogTestContext> options)
            : base(options)
    { }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<User> Users { get; set; }
}
