using MyBlog.Models;

namespace MyBlog.Areas.Chat.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        public List<Message> Messanges { get; set; } = new List<Message>();

    }
}
