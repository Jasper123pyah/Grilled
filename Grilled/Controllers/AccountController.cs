﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grilled.Data;
using Grilled.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Grilled.Controllers
{
    public class AccountController : Controller
    {
        AccountModel account = new AccountModel();
        private readonly GrilledContext context;
        
        public AccountController(GrilledContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
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
                return View(login);
            }
            return View(new LoginModel());
        }
        public ActionResult Register()
        {
            return View();
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

        public ActionResult Messages()
        {
            return View();
        }

        public ActionResult Items()
        {
            context.Database.EnsureCreated();

            account = context.Account.Where(a => a.Username == "Jasper").Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault(); // should be sessioned account
            
            return View(account);
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}
