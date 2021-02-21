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
            
            if (account.Favorites.Contains(context.Favorite.Where(f => f.ProductId == product.Id).FirstOrDefault()))
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
            return PartialView("Settings");
        }

        public ActionResult Messages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            context.Database.EnsureCreated();

            account = context.Account.FirstOrDefault(a => a.Username == login.Username);
            if (account != null && login.Password == account.Password)
            {
                HttpContext.Response.Cookies.Append("Login", account.Username);
                return RedirectToAction("Items","Account");
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

            account = context.Account.Where(a => a.Username == HttpContext.Request.Cookies["Login"]).Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault();
            
            display.Products = new List<ProductModel>();

            if (account == null)
                return View(display);

            foreach (ProductModel product in account.Products)
            {
                display.AddToDisplay(product, context, GetImagePath());
            }
            return View(display);
            //return PartialView("Items", display);
        }

    }
}
