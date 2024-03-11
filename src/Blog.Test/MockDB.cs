using projeto_final.Models;
using projeto_final.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.Test
{
public static class MockDB 
    {
    public static BlogContext GetContextInstanceForTests(string inMemoryDbName)
            {
                var contextOptions = new DbContextOptionsBuilder<BlogContext>()
                    .UseInMemoryDatabase(inMemoryDbName)
                    .Options;
                var context = new BlogContext(contextOptions);
                context.Users.AddRange(
                    GetUserListForTests()
                );
                context.Posts.AddRange(
                    GetPostListForTests()
                );
                context.SaveChanges();
                return context;
            }

            public static List<User> GetUserListForTests()
            {
                return new() {
                    new User{
                        Email = "teste@gmail.com",
                        Password = "123456789",
                        Username = "Usuário1",
                        UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    },
                    new User{
                        Email = "teste2@gmail.com",
                        Password = "123456789",
                        Username = "Usuário2",
                        UserId = new Guid("38d067e5-63af-4267-9d68-08dadd726893"),
                    },
                };
            }


            public static List<Post> GetPostListForTests()
            {
                return new() {
                    new Post{
                        Content = "Só um teste mesmo",
                        CreatedAt = DateTimeOffset.Now,
                        PostId = new Guid("3fa85f65-5717-4562-b3fc-2c963f66afa6"),
                        LastModified = DateTimeOffset.Now,
                        UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    },
                    new Post{
                        Content = "Mais um teste",
                        CreatedAt = DateTimeOffset.Now,
                        PostId = new Guid("7fe1bf74-ff24-49df-9c22-6656acc6eec3"),
                        LastModified = DateTimeOffset.Now,
                        UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    },
                    new Post{
                        Content = "Só mais um teste",
                        CreatedAt = DateTimeOffset.Now,
                        PostId = new Guid("dd484d39-53b1-4014-818a-cc7c130d813a"),
                        LastModified = DateTimeOffset.Now,
                        UserId = new Guid("38d067e5-63af-4267-9d68-08dadd726893"),
                    },
                };
            }
    }
    
   
        
       
}