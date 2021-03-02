using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Controllers
{
    public class ChatController : Controller
    {
        GrilledLogic.Chat chatLogic = new GrilledLogic.Chat();
        private readonly GrilledContext context;

        public ChatController(GrilledContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StartChat(string messagetext, string receivername, Guid productId)
        {
            chatLogic.StartChat(messagetext, receivername, productId, HttpContext, context);
            return RedirectToAction("BuyMessages");
        }

        [HttpPost]
        public ActionResult BuySend(string message, Guid messageId)
        {
            return RedirectToAction("BuyMessages", chatLogic.BuySend(message, messageId, HttpContext, context));
        }

        [HttpPost]
        public ActionResult SellSend(string message, Guid messageId)
        {
            return RedirectToAction("SellMessages", chatLogic.SellSend(message, messageId, HttpContext, context));
        }

        public ActionResult BuyMessages(ChatModel chat)
        {          
            return View("BuyMessages", chatLogic.BuyMessages(chat,HttpContext, context));
        }

        public ActionResult SellMessages(ChatModel chat)
        {          
            return View("SellMessages", chatLogic.SellMessages(chat, HttpContext, context));
        }
    }
}
