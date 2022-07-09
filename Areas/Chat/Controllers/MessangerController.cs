using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using MyBlog.Models;

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
        public async Task<IActionResult> Messanger()
        {
            User? user = _context.Users.Include(u => u.Chats)
                .ThenInclude(m => m.Members)
                .FirstOrDefault(u => u.Email == User.Identity.Name);

            string d1 = user.Email;

           // var chatsTitles;
            var chatsTitles  = (from chat in user.Chats
                                from member in chat.Members
                                where member.Email != User.Identity.Name
                                select member.Email).ToList();

            ViewBag.ChatsTitles = chatsTitles;

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Messanger(string username)
        {
            await ChatCreating(username);

            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            var chatsTitles = (from chat in user.Chats
                               from member in chat.Members
                               where member.Email != User.Identity.Name
                               select member.Email).ToList();

            ViewBag.ChatsTitles = chatsTitles;

            return View();
        }

        [NonAction]
        public async Task ChatCreating(string username)
        {
            User? userSender = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            User? userReceiver = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
            var newChat = new Models.Chat();

            _context.Chats.Add(newChat);

            userSender.Chats.Add(newChat);

            userReceiver.Chats.Add(newChat);

            _context.SaveChanges();
        }
    }
}
