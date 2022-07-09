using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public int? RoleInfoKey { get; set; }
        [ForeignKey("RoleInfoKey")]
        public Role? Role { get; set; }
        public 
    }
}
