using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GrilledData.Data;
using GrilledCommon.Models;

namespace Grilled.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GrilledContext context;
        GrilledLogic.Home homeLogic = new GrilledLogic.Home();

        public HomeController(ILogger<HomeController> logger, GrilledContext _context)
        {
            _logger = logger;
            context = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(homeLogic.IndexDisplay(HttpContext, context));
        }

        public IActionResult Search(string search, string category)
        {       
            return View(homeLogic.Search(search, category, HttpContext, context));
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
