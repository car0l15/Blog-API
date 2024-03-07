using Microsoft.EntityFrameworkCore;
using projeto_final.Models;
using System;

namespace projeto_final.Repository;

public class BlogContext : DbContext, BlogContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var server = Environment.GetEnvironmentVariable("DB_SERVER");
            var database = Environment.GetEnvironmentVariable("DB_DATABASE");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var trustServerCertificate = Environment.GetEnvironmentVariable("DB_TRUST_SERVER_CERTIFICATE");

            optionsBuilder.UseSqlServer($@"
                Server={server};
                Database={database};
                User={user};
                Password={password};
                TrustServerCertificate={trustServerCertificate};
            ").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);;
        }
    }
}
