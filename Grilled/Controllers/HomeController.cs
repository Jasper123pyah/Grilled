using Grilled.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grilled.Data;
using Microsoft.EntityFrameworkCore;

namespace Grilled.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GrilledContext context;
        DisplayProductModel display = new DisplayProductModel();

        public string GetImagePath()
        {
            return @"https://" + Request.Host.ToString() + @"/Uploads/";
        }

        public HomeController(ILogger<HomeController> logger, GrilledContext _context)
        {
            _logger = logger;
            context = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            context.Database.EnsureCreated();
            display.Products = new List<ProductModel>();
            
            foreach (ProductModel product in context.Product)
            {
                display.AddToDisplay(product, context, GetImagePath());
            }

            return View(display);
        }

        public IActionResult Search(string search, string category)
        {
            context.Database.EnsureCreated();
            display.Products = new List<ProductModel>();
            
            if (category == "All Categories")
            {
                if(search == null)
                {
                    foreach (ProductModel product in context.Product)
                    {
                        display.AddToDisplay(product, context, GetImagePath());
                    }
                }
                else
                {
                    foreach (ProductModel product in context.Product)
                    {
                        if (product.Name.ToLower().Contains(search.ToLower()) || product.Designer.ToLower().Contains(search.ToLower()) || product.Description.ToLower().Contains(search.ToLower()))
                        {
                            display.AddToDisplay(product, context, GetImagePath());
                        }
                    }
                }                  
            }
            else
            {
                foreach (ProductModel product in context.Product)
                {
                    if (product.Category == category)
                    {
                        if(search == null)
                        {
                            display.AddToDisplay(product, context, GetImagePath());
                        }    
                        else if (product.Name.ToLower().Contains(search.ToLower()) || product.Designer.ToLower().Contains(search.ToLower()) || product.Description.ToLower().Contains(search.ToLower()))
                        {
                            display.AddToDisplay(product, context, GetImagePath());
                        }
                    }
                }
            }
            return View(display);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
