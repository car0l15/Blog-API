using projeto_final.Models;

namespace projeto_final.Repository
{
    public interface ITryBlogRepository
    {
        public IEnumerable<User> GetAllUsers();
        public User GetUserByEmail(string email);
        public User GetUserById(Guid userId);
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);

        public IEnumerable<Post> GetPostsByUser(Guid userId);
        public Post GetPost(Guid postId);
        public void CreatePost(Post post);
        public void UpdatePost(Guid postId, Post post);
        public void DeletePost(Guid postId);
    }
}