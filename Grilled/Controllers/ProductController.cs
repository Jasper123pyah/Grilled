using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore;
using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Grilled.Controllers
{
    public class ProductController : Controller
    {
        private readonly GrilledContext context;
        private IWebHostEnvironment environment;
        GrilledLogic.Product productLogic = new GrilledLogic.Product();

        public ProductController(GrilledContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
            environment = _environment;
        }

        public ActionResult DeleteProduct(ProductModel product)
        {
            productLogic.Delete(product, context);
            return RedirectToAction("Items", "Account");
        }
        public ActionResult Details(ProductModel product)
        {
            return View(productLogic.Details(product, HttpContext, context));
        }
        public ActionResult Sell()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sell(ProductModel product, List<IFormFile> postedFiles)
        {
            if (productLogic.Sell(product, postedFiles, environment.WebRootPath, HttpContext, context))
                return RedirectToAction("Items", "Account");

            else
                return View();
        }

        [HttpGet]
        public ActionResult Edit(ProductModel product)
        {
            return View(productLogic.Edit(product, HttpContext, context));
        }

        [HttpPost]
        public ActionResult Change(ProductModel product)
        {     
            return RedirectToAction("Details", "Product", productLogic.Change(product, context));
        }


    }
}
