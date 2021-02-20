using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore;
using Grilled.Models;
using Grilled.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Grilled.Controllers
{
    public class ProductController : Controller
    {
        ProductModel product = new ProductModel();
        AccountModel account = new AccountModel();
        private readonly GrilledContext context;
        private IWebHostEnvironment environment;

        public string GetImagePath()
        {
            return @"https://" + Request.Host.ToString() + @"/Uploads/";
        }

        public ProductController(GrilledContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
            environment = _environment;
        }

        public ActionResult Details(ProductModel product)
        {
            ProductModel model = context.Product.Where(p => p.Id == product.Id).Include(a => a.Images).FirstOrDefault();
            model.Images[0].Source = GetImagePath() + model.Images[0].Name;
            return View(model);
        }
        public ActionResult Sell()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sell(ProductModel product, List<IFormFile> postedFiles)
        {
            context.Database.EnsureCreated();

            string loginId = HttpContext.Request.Cookies["Login"];
            account = context.Account.FirstOrDefault(a => a.Username == loginId);

            string path = Path.Combine(this.environment.WebRootPath, "Uploads");
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<Image> uploadedFiles = new List<Image>();

            foreach (IFormFile postedFile in postedFiles)
            {
                if (postedFile.FileName.EndsWith(".jpg") || postedFile.FileName.EndsWith(".png"))
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(new Image()
                        {
                            Name = fileName,
                        });
                    }
                }
                else
                    return View();
            }

            if (account != null)
            {
                if (account.Products == null)
                    account.Products = new List<ProductModel>();

                account.Products.Add(new ProductModel()
                {
                    Name = product.Name,
                    Category = product.Category,
                    Designer = product.Designer,
                    Size = product.Size,
                    Color = product.Color,
                    Condition = product.Condition,
                    Description = product.Description,
                    Price = product.Price,
                    Shipping = product.Shipping,
                    Images = uploadedFiles
                });
                context.SaveChanges();

                return RedirectToAction("Items", "Account");
            }
            else
            {
                return View(new ProductModel());
            }
        }
    }
}
