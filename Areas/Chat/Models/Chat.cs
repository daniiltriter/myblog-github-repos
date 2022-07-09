using MyBlog.Models;

namespace MyBlog.Areas.Chat.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public User[] Users { get; set; } = new User[2];
    }
}
