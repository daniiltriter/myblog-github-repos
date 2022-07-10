using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using MyBlog.Models;
using System.Linq;

namespace MyBlog.Areas.Chat.Controllers
{  
    public class MessangerController : Controller
    {
        private ApplicationContext _context;

        public MessangerController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Messanger()
        {
            ViewBag.ChatsTitles = ChatsTitles().ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Messanger(string username)
        {
            await ChatCreating(username);

            ViewBag.ChatsTitles = ChatsTitles().ToList();
            return View();
        }
        #region Non Actions (SQL Methods)
        [NonAction]
        public async Task ChatCreating(string username)
        {
            User? userSender = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            User? userReceiver = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);

            if (userSender is not null && userReceiver is not null)
            {
                var newChat = new Models.Chat();
                _context.Chats.Add(newChat);
                userSender.Chats.Add(newChat);
                userReceiver.Chats.Add(newChat);

                _context.SaveChanges();
            }
        }
        [NonAction]
        public IEnumerable<string> ChatsTitlesTest(User user)
        {
           return from chat in user.Chats
             from member in chat.Members
             where member.Email != User.Identity.Name
             select member.Email;
        }
        [NonAction]
        public IEnumerable<string> ChatsTitles(User user)
        {
            return user.Chats.SelectMany(c => c.Members, (c, l) => new { Member = l })
                .Select(n => n.Member.Email)
                .Where(n => n != User.Identity.Name);
        }
        [NonAction]
        public IQueryable<string> ChatsTitles()
        {
            return _context.Users.Include(c => c.Chats)
                .ThenInclude(m => m.Members)
                .Where(u => u.Email == User.Identity.Name)
                .SelectMany(u => u.Chats)
                .SelectMany(e => e.Members)
                .Select(m=>m.Email)
                .Where(e => e != User.Identity.Name);
        }
        #endregion
    }
}
