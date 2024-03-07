namespace projeto_final.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
        public Guid PostId { get; set; }

        public string? Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public Guid? UserId { get; set; }
}
