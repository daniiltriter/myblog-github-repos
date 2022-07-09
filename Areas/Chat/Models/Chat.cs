using MyBlog.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Areas.Chat.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public User[] Users { get; set; }

        public Chat()
        {
            Users = new User[2];
        }
    }
}
