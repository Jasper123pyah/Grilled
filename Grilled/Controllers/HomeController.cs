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
                ProductModel productAdd = context.Product.Where(p => p.Id == product.Id).Include(a => a.Images).FirstOrDefault();
                display.Products.Add(productAdd);
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
