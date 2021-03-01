using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grilled.Data;
using Grilled.Models;
using Microsoft.AspNetCore.Hosting;
using System.Runtime;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;

namespace Grilled.Controllers
{
    public class AccountController : Controller
    { 
        AccountModel account = new AccountModel();
        ChatModel chat = new ChatModel();
        private readonly GrilledContext context;
        DisplayProductModel display = new DisplayProductModel();

        public string GetImagePath()
        {
            return @"https://" + Request.Host.ToString() + @"/Uploads/";
        }

        public AccountController(GrilledContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
        }

        public ActionResult Index()
        {
            if(HttpContext.Request.Cookies["Login"] == null)
                return RedirectToAction("Login");
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            if(HttpContext.Request.Cookies["Login"] != null)
            {
                HttpContext.Response.Cookies.Delete("Login");
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Favorite()
        {
            context.Database.EnsureCreated();

            account = context.Account.Where(a => a.Username == HttpContext.Request.Cookies["Login"]).Include(a => a.Favorites).Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault(); 

            display.Products = new List<ProductModel>();

            if (account == null)
                return View(display);

            if (account.Favorites == null)
                return View(display);

            foreach (FavoriteModel favorite in account.Favorites)
            {
                ProductModel product = context.Product.Where(p => p.Id == favorite.ProductId).Include(a => a.Images).FirstOrDefault();
                display.AddToDisplay(product, context, GetImagePath());
            }

            return View(display);
        }

        public ActionResult Save(ProductModel product)
        {
            context.Database.EnsureCreated();
            account = context.Account.Where(a => a.Username == HttpContext.Request.Cookies["Login"]).Include(a => a.Favorites).Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault();
            
            if(account.Favorites.FirstOrDefault(a => a.ProductId == product.Id) != null)
                context.Favorite.Remove(context.Favorite.Where(f => f.ProductId == product.Id).FirstOrDefault());

            else
            {
                FavoriteModel favorite = new FavoriteModel() { ProductId = product.Id };
                favorite.AddFavToAccount(account);
            }
            context.SaveChanges();
            return RedirectToAction("Favorite");
        }

        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StartChat(string messagetext, string receivername, Guid productId)
        {
            context.Database.EnsureCreated();
            account = context.Account.FirstOrDefault(a => a.Username == HttpContext.Request.Cookies["Login"]);
            AccountModel raccount = context.Account.FirstOrDefault(a => a.Username == receivername);
            

            chat = context.Chat.Where(c => c.Users == $"{account.Username}_{raccount.Username}" 
                                        || c.Users == $"{raccount.Username}_{account.Username}" 
                                        && c.ProductId == productId)
                                        .Include(a => a.Messages).FirstOrDefault();

            if(chat != null)
            {         
                chat.Messages.Add(new MessageModel()
                {
                    Message = messagetext,
                    SenderName = account.Username,
                    Time = DateTime.Now
                });
            }
            else
            {
                chat = new ChatModel()
                {
                    ProductId = productId,
                    Users = $"{account.Username}_{raccount.Username}",
                    Messages = new List<MessageModel>()
                };

                chat.Messages.Add(new MessageModel()
                {
                    Message = messagetext,
                    SenderName = account.Username,
                    Time = DateTime.Now
                });

                if (account.BuyChats != null)
                    account.BuyChats.Add(chat);
                else
                    account.BuyChats = new List<ChatModel>() { chat };

                if (raccount.SellChats != null)
                    raccount.SellChats.Add(chat);
                else
                    raccount.SellChats = new List<ChatModel> { chat };
                context.Chat.Add(chat);
            }
            context.SaveChanges();

            return RedirectToAction("BuyMessages");
        }

        [HttpPost]
        public ActionResult BuySend(string message, Guid messageId)
        {
            context.Database.EnsureCreated();

            MessageModel searchmessage = context.Message.FirstOrDefault(a => a.Id == messageId);
            ChatModel chat = context.Chat.Include(a => a.Messages).FirstOrDefault(a => a.Messages.Contains(searchmessage));

            if (chat != null)
            {
                chat.Messages.Add(new MessageModel()
                {
                    Message = message,
                    SenderName = HttpContext.Request.Cookies["Login"],
                    Time = DateTime.Now
                });
            }
            context.SaveChanges();

            return RedirectToAction("BuyMessages", chat);
        }
        [HttpPost]
        public ActionResult SellSend(string message, Guid messageId)
        {
            context.Database.EnsureCreated();

            MessageModel searchmessage = context.Message.FirstOrDefault(a => a.Id == messageId);
            ChatModel chat = context.Chat.Include(a => a.Messages).FirstOrDefault(a => a.Messages.Contains(searchmessage));

            if (chat != null)
            {
                chat.Messages.Add(new MessageModel()
                {
                    Message = message,
                    SenderName = HttpContext.Request.Cookies["Login"],
                    Time = DateTime.Now
                });
            }
            context.SaveChanges();

            return RedirectToAction("SellMessages", chat);
        }

        public ActionResult BuyMessages(ChatModel chat)
        {
            context.Database.EnsureCreated();
            account = context.Account.Include(a => a.BuyChats).ThenInclude(a => a.Messages)
                                     .FirstOrDefault(a => a.Username == HttpContext.Request.Cookies["Login"]);

            DisplayMessagesModel mdisplay = new DisplayMessagesModel
            {
                Chats = account.BuyChats,
                Messages = new List<MessageModel>()
            };

            if (chat != null)
            {
                ChatModel selectedchat = context.Chat.Include(c => c.Messages).FirstOrDefault(c => c.Id == chat.Id);
                if (selectedchat != null)
                {
                    foreach (MessageModel message in selectedchat.Messages)
                    {
                        mdisplay.Messages.Add(message);
                    }
                    mdisplay.Messages = mdisplay.Messages.OrderBy(x => x.Time).ToList();
                }
            }

            return View(mdisplay);
        }

        public ActionResult SellMessages(ChatModel chat)
        {
            context.Database.EnsureCreated();
            account = context.Account.Include(a => a.SellChats).ThenInclude(a => a.Messages)
                                     .FirstOrDefault(a => a.Username == HttpContext.Request.Cookies["Login"]);

            DisplayMessagesModel mdisplay = new DisplayMessagesModel
            {
                Chats = account.SellChats,
                Messages = new List<MessageModel>()
            };
            if (chat != null)
            {
                ChatModel selectedchat = context.Chat.Include(c => c.Messages).FirstOrDefault(c => c.Id == chat.Id);
                if (selectedchat != null)
                {
                    foreach (MessageModel message in selectedchat.Messages)
                    {
                        mdisplay.Messages.Add(message);
                    }
                    mdisplay.Messages = mdisplay.Messages.OrderBy(x => x.Time).ToList();
                }
            }


            return View(mdisplay);
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            context.Database.EnsureCreated();

            account = context.Account.FirstOrDefault(a => a.Username == login.Username);
            if (account != null && login.Password == account.Password)
            {
                HttpContext.Response.Cookies.Append("Login", account.Username);
                return RedirectToAction("Index");
            }
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel registration)
        {
            context.Database.EnsureCreated();

            account = context.Account.FirstOrDefault(a => a.Username == registration.Username);
            if (account == null)
            {
                context.Account.Add(new AccountModel()
                {
                    Username = registration.Username,
                    Password = registration.Password,
                    Email = registration.Email
                });
                context.SaveChanges();

                return RedirectToAction("Login");
            }
            else
            {
                return View(new RegistrationModel());
            }
        }

        public ActionResult Items()
        {
            context.Database.EnsureCreated();

            account = context.Account.Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault(a => a.Username == HttpContext.Request.Cookies["Login"]);
            
            display.Products = new List<ProductModel>();

            if (account == null)
                return View(display);

            foreach (ProductModel product in account.Products)
            {
                display.AddToDisplay(product, context, GetImagePath());
            }
            return View(display);
        }

    }
}
