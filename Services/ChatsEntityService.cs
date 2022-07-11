using Microsoft.EntityFrameworkCore;
using MyBlog.Models;
using System.Security.Claims;
using MyBlog.Areas.Chat.Models;

namespace MyBlog.Services
{
    public class ChatsEntityService
    {
        private ApplicationContext _context;
        public ChatsEntityService(ApplicationContext context)
        {
            _context = context;
        }

        // IQueryable перегрузка метода для вытаскивания заголовков чатов
        public IQueryable<string> ChatsTitles(string activeUserName)
        {
            
            return _context.Users.Include(c => c.Chats)
                .ThenInclude(m => m.Members)
                .Where(u => u.Email == activeUserName)
                .SelectMany(u => u.Chats)
                .SelectMany(e => e.Members)
                .Select(m => m.Email)
                .Where(e => e != activeUserName);
        }

        // (не используется) IEnumerable перегрузка метода для вытаскивания заголовков чатов
        public IEnumerable<string> ChatsTitles(User user, string activeUserName)
        {
            return user.Chats.SelectMany(c => c.Members, (c, l) => new { Member = l })
                .Select(n => n.Member.Email)
                .Where(n => n != activeUserName);
        }

        // метод для создания чата между пользователями
        public async Task ChatCreating(string receiverName, string activeUserName)
        {
            User? userSender = await _context.Users.FirstOrDefaultAsync(u => u.Email == activeUserName);
            User? userReceiver = await _context.Users.FirstOrDefaultAsync(u => u.Email == receiverName);
            
            if (userSender is not null && userReceiver is not null)
            {
                var newChat = new Chat();
                _context.Chats.Add(newChat);
                userSender.Chats.Add(newChat);
                userReceiver.Chats.Add(newChat);

                _context.SaveChanges();
            }
        }

        // (не используется) IEnumerable перегрузка метода для вытаскивания заголовков чатов
        // linq запросы
        public IEnumerable<string> ChatsTitles(User user, bool b, string activeUserName)
        {
            return from chat in user.Chats
                   from member in chat.Members
                   where member.Email != activeUserName
                   select member.Email;
        }
    }
}
