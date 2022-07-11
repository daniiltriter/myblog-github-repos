using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using MyBlog.Models;
using System.Linq;
using MyBlog.Services;

namespace MyBlog.Areas.Chat.Controllers
{  
    public class MessangerController : Controller
    {
        private ChatsEntityService _chatsService;

        public MessangerController(ChatsEntityService chatsService)
        {
            _chatsService = chatsService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Messanger()
        {
            ViewBag.ChatsTitles = _chatsService.ChatsTitles(User.Identity.Name).ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Messanger(string receiverName)
        {
            await _chatsService.ChatCreating(receiverName, User.Identity.Name);

            ViewBag.ChatsTitles = _chatsService.ChatsTitles(User.Identity.Name).ToList();
            return View();
        }
    }
}
