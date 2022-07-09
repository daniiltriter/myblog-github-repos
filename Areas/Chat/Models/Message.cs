namespace MyBlog.Areas.Chat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
