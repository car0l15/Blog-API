namespace projeto_final.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
}
