using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Runtime;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using GrilledCommon.Models;
using GrilledData.Data;

namespace Grilled.Controllers
{
    public class AccountController : Controller
    { 
        GrilledLogic.Account accountLogic = new GrilledLogic.Account();
        private readonly GrilledContext context;
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
            return View(accountLogic.Favorite(HttpContext, context));
        }

        public ActionResult Save(ProductModel product)
        {
            accountLogic.Save(HttpContext, product, context);
            return RedirectToAction("Favorite");
        }

        public ActionResult Settings()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if(accountLogic.Login(login, HttpContext, context))
                return RedirectToAction("Index");

            else
                return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel registration)
        {
            if (accountLogic.Register(registration, context))
                return RedirectToAction("Login");

            else
                return View(new RegistrationModel());
        }

        public ActionResult Items()
        {     
            return View(accountLogic.Items(HttpContext, context));
        }

    }
}
